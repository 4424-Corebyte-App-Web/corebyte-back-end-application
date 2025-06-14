<<<<<<< Updated upstream
using Corebyte_platform.history_status.Application.Infernal.CommandServices;
using Corebyte_platform.history_status.Application.Infernal.QueryServices;
using Corebyte_platform.history_status.Domain.Repositories;
using Corebyte_platform.history_status.Domain.Services;
using Corebyte_platform.history_status.Infrastucture.Repositories;
using Corebyte_platform.Shared.Domain.Repositories;
using Corebyte_platform.Shared.Infrastructure.Interfaces.ASP.Configuration;
using Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Configuration;
using Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Repositories;
using EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Corebyte_platform.batch_management.Common.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Configure Lower Case URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add services to the container.
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());
=======
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Corebyte_platform.batch_management.Application.Infernal.CommandServices;
using Corebyte_platform.batch_management.Application.Infernal.QueryServices;
using Corebyte_platform.batch_management.Domain.Repositories;
using Corebyte_platform.batch_management.Infrastructure.Data;
using Corebyte_platform.batch_management.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { 
        Title = "Corebyte Platform API", 
        Version = "v1",
        Description = "Backend API for Corebyte Platform"
    });
});

// Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        mysqlOptions =>
        {
            mysqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }
    );
});

// Register Batch Management services
builder.Services.AddScoped<IBatchRepository, BatchRepository>();
builder.Services.AddScoped<IBatchCommandService, BatchCommandService>();
builder.Services.AddScoped<IBatchQueryService, BatchQueryService>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") // Your React app URL
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
>>>>>>> Stashed changes

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


// News Bounded Context Injection Configuration
builder.Services.AddScoped<IHistoryRepository, HistoryRepository>();
builder.Services.AddScoped<IHistoryCommandService, HistoryCommandService>();
builder.Services.AddScoped<IHistoryQueryService, HistoryQueryService>();

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
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Corebyte Platform API v1"));
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowReactApp");
app.UseAuthorization();

// Register global exception handling middleware
app.UseGlobalExceptionHandling();
app.MapControllers();

// Initialize Database
try
{
    var scope = app.Services.CreateScope();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    // Apply any pending migrations
    dbContext.Database.Migrate();
    
    // Seed test data
    await DatabaseSeeder.SeedTestDataAsync(dbContext, logger);
    
    logger.LogInformation("Database initialization and seeding completed successfully");
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during database initialization");
}

app.Run();
