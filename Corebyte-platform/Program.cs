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
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

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
builder.Services.AddMediatR(typeof(CreateBatchCommand));

// replenishment bounded context DI
builder.Services.AddScoped<IReplenishmentRepository, ReplenishmentRepository>();
builder.Services.AddScoped<IReplenishmentCommandService, ReplenishmentCommandService>();
builder.Services.AddScoped<IReplenishmentQueryService, ReplenishmentQueryService>();

// authentication bounded context DI
// Authentication Bounded Context Injection
builder.Services.AddScoped<Corebyte_platform.authentication.Domain.Repositories.IUserRepository, 
    Corebyte_platform.authentication.Infrastucture.Repositories.UserRepository>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<RegistrationService>();

// Add Controllers from Authentication Bounded Context
builder.Services.AddControllers()
    .AddApplicationPart(typeof(Corebyte_platform.authentication.Interfaces.REST.AuthController).Assembly);

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSettings["Key"];
var jwtIssuer = jwtSettings["Issuer"] ?? "https://localhost:5001";
var jwtAudience = jwtSettings["Audience"] ?? "https://localhost:5001";

if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 32)
{
    // Generate a secure random key if none is provided (for development only)
    if (builder.Environment.IsDevelopment())
    {
        var keyBytes = new byte[32]; // 256 bits
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(keyBytes);
        jwtKey = Convert.ToBase64String(keyBytes);
        Console.WriteLine($"WARNING: Using auto-generated JWT key for development: {jwtKey}");
    }
    else
    {
        throw new InvalidOperationException("JWT Key must be at least 32 characters long");
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

var app= builder.Build();

// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Initialize database on startup
try
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    Console.WriteLine("Verificando base de datos...");
    
    // Ensure database exists and apply migrations
    await context.Database.MigrateAsync();
    
    Console.WriteLine("Base de datos y tablas verificadas.");
    
    // Database is now ready to use
}
catch (Exception ex)
{
    Console.WriteLine($"Error al inicializar la base de datos: {ex.Message}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"Detalles: {ex.InnerException.Message}");
    }
    // Continue execution to show error in the API
}

// Authentication & Authorization Middleware
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("VueCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
