using Microsoft.EntityFrameworkCore;
using Corebyte_platform.batch_management.Domain.Model.Aggregates;

namespace Corebyte_platform.batch_management.Infrastructure.Data
{
    /// <summary>
    /// Database context for batch management
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructor for ApplicationDbContext
        /// </summary>
        /// <param name="options">The DbContext options</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet for Batch entities
        /// </summary>
        public DbSet<Batch> Batches { get; set; }

        /// <summary>
        /// Configure the model using Fluent API
        /// </summary>
        /// <param name="modelBuilder">The model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Temperature)
                    .HasMaxLength(20);

                entity.Property(e => e.Amount)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Total)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Date)
                    .IsRequired();

                entity.Property(e => e.NLote)
                    .IsRequired()
                    .HasMaxLength(20);

                // Add additional indexes for commonly queried fields
                entity.HasIndex(e => e.NLote);
                entity.HasIndex(e => e.Type);
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.Date);
            });
        }
    }
}

