using Corebyte_platform.Shared.Domain.Repositories;
using Corebyte_platform.Shared.Infrastructure.Interfaces.ASP.Configuration;
using Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Configuration;
using Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Repositories;
using EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Corebyte_platform.batch_management.Infrastucture;
using Corebyte_platform.batch_management.Domain.Repositories;
using Corebyte_platform.batch_management.Domain.Services;
using Corebyte_platform.batch_management.Infrastucture.Repositories;
using MediatR;
using Corebyte_platform.batch_management.Application.Infernal.CommandServices;


var builder = WebApplication.CreateBuilder(args);

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
    builder.Services.AddDbContext<AppDbContext>(options =>
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


// News Bounded Context Injection Configuration

// Batch-Management bounded context DI
builder.Services.AddDbContext<BatchContext>(options =>
    options.UseMySQL(connectionString));
builder.Services.AddScoped<IBatchRepository, BatchRepository>();
builder.Services.AddScoped<BatchService>();
builder.Services.AddMediatR(typeof(CreateBatchCommand));

var app= builder.Build();


// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
