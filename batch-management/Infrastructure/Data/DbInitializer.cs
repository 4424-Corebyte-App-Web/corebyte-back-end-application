using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Corebyte_platform.batch_management.Infrastructure.Data
{
    /// <summary>
    /// Database initializer for seeding initial data
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Initialize the database with seed data
        /// </summary>
        /// <param name="serviceProvider">The service provider</param>
        /// <param name="logger">The logger</param>
        public static void Initialize(IServiceProvider serviceProvider, ILogger logger)
        {
            try
            {
                var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                logger.LogInformation("Starting database initialization");
                
                // Add seed data if needed
                // For example:
                // if (!dbContext.Batches.Any())
                // {
                //     dbContext.Batches.AddRange(
                //         new Batch { ... },
                //         new Batch { ... }
                //     );
                //     dbContext.SaveChanges();
                // }
                
                logger.LogInformation("Database initialization completed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred during database initialization");
                throw;
            }
        }
    }
}

