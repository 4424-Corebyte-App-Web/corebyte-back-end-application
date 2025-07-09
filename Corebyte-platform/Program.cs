using Corebyte_platform.batch_management.Application.Infernal.CommandServices;
using Corebyte_platform.orders.Application.Infernal.CommandServices;
using Corebyte_platform.orders.Application.Infernal.QueryServices;
using Corebyte_platform.orders.Domain.Repositories;
using Corebyte_platform.orders.Domain.Services;
using Corebyte_platform.orders.Infrastucture.Persistence.EFC.Repositories;

using Corebyte_platform.history_status.Application.Infernal.CommandServices;
using Corebyte_platform.history_status.Application.Infernal.QueryServices;
using Corebyte_platform.history_status.Domain.Repositories;
using Corebyte_platform.history_status.Domain.Services;
using Corebyte_platform.history_status.Infrastucture.Persistence.EFC.Repositories;
using Corebyte_platform.history_status.Infrastucture.Repositories;


using Corebyte_platform.batch_management.Infrastucture;
using Corebyte_platform.batch_management.Domain.Repositories;
using Corebyte_platform.batch_management.Domain.Services;
using Corebyte_platform.batch_management.Infrastucture.Repositories;

using Corebyte_platform.replenishment.Application.Internal.CommandServices;
using Corebyte_platform.replenishment.Application.Internal.QueryServices;
using Corebyte_platform.replenishment.Domain.Respositories;
using Corebyte_platform.replenishment.Domain.Services;
using Corebyte_platform.replenishment.Infrastructure.Persistence.EFC.Repositories;

using Corebyte_platform.Shared.Domain.Repositories;
using Corebyte_platform.Shared.Infrastructure.Interfaces.ASP.Configuration;
using Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Configuration;
using Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Repositories;

using Corebyte_platform.authentication.Application;
using Corebyte_platform.authentication.Domain.Repositories;
using Corebyte_platform.authentication.Infrastucture.Repositories;

using EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Corebyte_platform.authentication.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Corebyte_platform.IAM.Infrastructure.Tokens.JWT.Configuration;
using Corebyte_platform.IAM.Domain.Services;
using Corebyte_platform.IAM.Application.Internal.CommandServices;
using Corebyte_platform.IAM.Application.Internal.OutboundServices;
using Corebyte_platform.IAM.Application.Internal.QueryServices;
using Corebyte_platform.IAM.Infrastructure.Tokens.JWT.Services;
using Corebyte_platform.IAM.Infrastructure.Hashing.BCrypt.Services;
using Corebyte_platform.IAM.Interfaces.ACL.Services;
using Corebyte_platform.IAM.Interfaces.ACL;
using Cortex.Mediator.Commands;
using Cortex.Mediator.Behaviors;
using Cortex.Mediator.DependencyInjection;
// Cortex.Mediator references removed as they might conflict with MediatR

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("VueCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader();
    });
});

// Configure Lower Case URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString is null)
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

// Configure Database Context and Logging Levels
if (builder.Environment.IsDevelopment())
    builder.Services.AddDbContext<AppDbContext>(
        options =>
        {
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });
else if (builder.Environment.IsProduction())
    builder.Services.AddDbContext<AppDbContext>(
        options =>
        {
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Error)
                .EnableDetailedErrors();
        });

// Configure Dependency Injection

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// Order Bounded Context Injection Configuration
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderCommandService, OrderCommandService>();
builder.Services.AddScoped<IOrderCommandService>(provider =>
    new OrderCommandService(
        provider.GetRequiredService<IOrderRepository>(),

        provider.GetRequiredService<IUnitOfWork>()
    )
);
builder.Services.AddScoped<IOrderQueryService, OrderQueryService>();

// History Bounded Context Injection Configuration
builder.Services.AddScoped<IHistoryRepository, HistoryRepository>();
builder.Services.AddScoped<IHistoryCommandService>(provider =>
    new HistoryCommandService(
        provider.GetRequiredService<IHistoryRepository>(),
        provider.GetRequiredService<IUnitOfWork>()
    )
);
builder.Services.AddScoped<IHistoryQueryService, HistoryQueryService>();

// Record Bounded Context Injection Configuration
builder.Services.AddScoped<IRecordRepository, RecordRepository>();
builder.Services.AddScoped<IRecordCommandService>(provider =>
    new RecordCommandService(
        provider.GetRequiredService<IRecordRepository>(),
        provider.GetRequiredService<IUnitOfWork>()
    )
);
builder.Services.AddScoped<IRecordQueryService, RecordQueryService>();

// Batch-Management bounded context DI
builder.Services.AddDbContext<BatchContext>(options =>
    options.UseMySQL(connectionString));
builder.Services.AddScoped<IBatchRepository, BatchRepository>();
builder.Services.AddScoped<BatchService>();
// Register MediatR with the assembly containing your commands/handlers
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateBatchCommand).Assembly));

// replenishment bounded context DI
builder.Services.AddScoped<IReplenishmentRepository, ReplenishmentRepository>();
builder.Services.AddScoped<IReplenishmentCommandService, ReplenishmentCommandService>();
builder.Services.AddScoped<IReplenishmentQueryService, ReplenishmentQueryService>();

// Configure JWT settings
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

// Register IAM services
builder.Services.AddScoped<Corebyte_platform.IAM.Domain.Repositories.IUserRepository, Corebyte_platform.IAM.Infrastructure.Persistence.EFC.Repositories.UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

// Register Authentication services
builder.Services.AddScoped<Corebyte_platform.authentication.Domain.Repositories.IUserRepository, Corebyte_platform.authentication.Infrastucture.Repositories.UserRepository>();
builder.Services.AddScoped<Corebyte_platform.authentication.Application.Services.LoginService>();
builder.Services.AddScoped<Corebyte_platform.authentication.Application.Services.RegistrationService>();

// Add Mediator for CQRS
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));
builder.Services.AddCortexMediator(
    configuration: builder.Configuration,
    handlerAssemblyMarkerTypes: new[] { typeof(Program) }, configure: options =>
    {
        options.AddOpenCommandPipelineBehavior(typeof(LoggingCommandBehavior<>));
        //options.AddDefaultBehaviors();
    });

// Configure Controllers with KebabCase and include AuthController assembly
builder.Services
    .AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddApplicationPart(typeof(Corebyte_platform.authentication.Interfaces.REST.AuthController).Assembly);

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSettings["Key"];
var jwtIssuer = jwtSettings["Issuer"] ?? (builder.Environment.IsDevelopment() ? "https://localhost:5001" : null);
var jwtAudience = jwtSettings["Audience"] ?? (builder.Environment.IsDevelopment() ? "https://localhost:5001" : null);

// Validate JWT configuration based on environment
if (string.IsNullOrWhiteSpace(jwtKey))
{
    if (builder.Environment.IsDevelopment())
    {
        // Generate a secure random key for development only
        var keyBytes = new byte[32]; // 256 bits
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(keyBytes);
        jwtKey = Convert.ToBase64String(keyBytes);
        Console.WriteLine($"WARNING: Using auto-generated JWT key for development: {jwtKey}");
    }
    else
    {
        throw new InvalidOperationException("JWT Key is required and must be at least 32 characters long in production. Please set the 'Jwt:Key' configuration value.");
    }
}
else if (jwtKey.Length < 32)
{
    throw new InvalidOperationException("JWT Key must be at least 32 characters long");
}

// Validate Issuer and Audience in production
if (!builder.Environment.IsDevelopment())
{
    if (string.IsNullOrWhiteSpace(jwtIssuer) || string.IsNullOrWhiteSpace(jwtAudience))
    {
        throw new InvalidOperationException("Both JWT Issuer and Audience must be configured in production. Please set 'Jwt:Issuer' and 'Jwt:Audience' in your production configuration.");
    }
    
    if (jwtIssuer.Contains("localhost") || jwtAudience.Contains("localhost"))
    {
        throw new InvalidOperationException("JWT Issuer and Audience cannot contain 'localhost' in production. Please use your production domain.");
    }
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.Zero // Remove delay of token when expire
    };

    // For development: Log token validation errors
    if (builder.Environment.IsDevelopment())
    {
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Authentication failed: {context.Exception}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token validated successfully");
                return Task.CompletedTask;
            }
        };
    }
});

// Add Authorization
builder.Services.AddAuthorization();

var app = builder.Build();

// Initialize database on startup
try
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    Console.WriteLine("Verificando base de datos...");
    await context.Database.MigrateAsync();
    Console.WriteLine("Base de datos y tablas verificadas.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error al inicializar la base de datos: {ex.Message}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"Detalles: {ex.InnerException.Message}");
    }
}
// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("VueCorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();




