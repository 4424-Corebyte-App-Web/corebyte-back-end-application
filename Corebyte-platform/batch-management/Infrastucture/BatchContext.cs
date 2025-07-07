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
            modelBuilder.Entity<Batch>(entity =>
            {
                entity.ToTable("Batches");
                
                // Configure Name as the primary key
                entity.HasKey(b => b.Name);
                
                // Configure properties
                entity.Property(b => b.Name)
                    .IsRequired()
                    .HasMaxLength(255);
                    
                entity.Property(b => b.NLote)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("n_lote");
                    
                entity.Property(b => b.Type)
                    .IsRequired()
                    .HasMaxLength(100);
                    
                entity.Property(b => b.Status)
                    .IsRequired()
                    .HasMaxLength(100);
                    
                entity.Property(b => b.Amount)
                    .IsRequired()
                    .HasMaxLength(50);
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}

