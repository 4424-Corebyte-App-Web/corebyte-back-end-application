using Microsoft.Extensions.Logging;
using Corebyte_platform.batch_management.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corebyte_platform.batch_management.Infrastructure.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedTestDataAsync(ApplicationDbContext context, ILogger logger)
        {
            try
            {
                // Check if we already have data
                if (await context.Batches.AnyAsync())
                {
                    logger.LogInformation("Database already contains test data. Skipping seed.");
                    return;
                }

                // Clear existing data if any
                context.Batches.RemoveRange(await context.Batches.ToListAsync());
                await context.SaveChangesAsync();

                var testBatches = new List<Batch>
                {
                    // Records that match the query criteria (Invoice, Pending, within date range)
                    new Batch
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Test Invoice 1",
                        Type = "Invoice",
                        Status = "Pending",
                        Temperature = "20C",
                        Amount = "100",
                        Total = 1500.50m,
                        Date = DateTime.Parse("2025-06-05T10:00:00"),
                        NLote = "LOT001"
                    },
                    new Batch
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Test Invoice 2",
                        Type = "Invoice",
                        Status = "Pending",
                        Temperature = "22C",
                        Amount = "50",
                        Total = 750.25m,
                        Date = DateTime.Parse("2025-06-08T14:30:00"),
                        NLote = "LOT002"
                    },
                    new Batch
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Test Invoice 3",
                        Type = "Invoice",
                        Status = "Pending",
                        Temperature = "21C",
                        Amount = "75",
                        Total = 1125.75m,
                        Date = DateTime.Parse("2025-06-03T09:15:00"),
                        NLote = "LOT003"
                    },

                    // Records that should NOT appear (different dates, types, or status)
                    new Batch
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Outside Date Range",
                        Type = "Invoice",
                        Status = "Pending",
                        Temperature = "20C",
                        Amount = "100",
                        Total = 1500.50m,
                        Date = DateTime.Parse("2025-05-30T10:00:00"),
                        NLote = "LOT004"
                    },
                    new Batch
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Different Type",
                        Type = "Receipt",
                        Status = "Pending",
                        Temperature = "21C",
                        Amount = "60",
                        Total = 900.00m,
                        Date = DateTime.Parse("2025-06-05T10:00:00"),
                        NLote = "LOT005"
                    },
                    new Batch
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Different Status",
                        Type = "Invoice",
                        Status = "Completed",
                        Temperature = "22C",
                        Amount = "80",
                        Total = 1200.00m,
                        Date = DateTime.Parse("2025-06-07T10:00:00"),
                        NLote = "LOT006"
                    },

                    // Edge case records
                    new Batch
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Start Date Edge",
                        Type = "Invoice",
                        Status = "Pending",
                        Temperature = "20C",
                        Amount = "90",
                        Total = 1350.00m,
                        Date = DateTime.Parse("2025-06-01T00:00:01"),
                        NLote = "LOT007"
                    },
                    new Batch
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "End Date Edge",
                        Type = "Invoice",
                        Status = "Pending",
                        Temperature = "21C",
                        Amount = "110",
                        Total = 1650.00m,
                        Date = DateTime.Parse("2025-06-10T23:59:58"),
                        NLote = "LOT008"
                    }
                };

                // Add all batches to context
                await context.Batches.AddRangeAsync(testBatches);

                // Save changes
                await context.SaveChangesAsync();

                logger.LogInformation("Successfully seeded test data with {Count} batch records", testBatches.Count);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database with test data");
                throw;
            }
        }
    }
}

