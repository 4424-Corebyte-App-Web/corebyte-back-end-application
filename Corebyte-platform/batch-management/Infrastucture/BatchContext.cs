using Microsoft.EntityFrameworkCore;
using Corebyte_platform.batch_management.Domain.Model.Aggregates;

namespace Corebyte_platform.batch_management.Infrastucture
{
    public class BatchContext : DbContext
    {
        public DbSet<Batch> Batches { get; set; }

        public BatchContext(DbContextOptions<BatchContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Batch>().ToTable("Batches");
            base.OnModelCreating(modelBuilder);
        }
    }
}

