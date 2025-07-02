using EntityFrameworkCore;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Configuration.Extensions;
using Corebyte_platform.history_status.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Corebyte_platform.history_status.Domain.Model.ValueObjects;
using Corebyte_platform.Shared.Infrastructure.Persistence.EFC.Converters;



namespace Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Configuration
{
    /// <summary>
    ///     Application database context
    /// </summary>
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            // Add the created and updated interceptor
            builder.AddCreatedUpdatedInterceptor();
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Configuration of the entity History
            builder.Entity<History>(entity =>
            {
                entity.HasKey(h => h.Id);
                entity.Property(h => h.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(h => h.customer).IsRequired().HasMaxLength(100);
                entity.Property(h => h.date).IsRequired();
                entity.Property(h => h.product).IsRequired().HasMaxLength(100);
                entity.Property(h => h.amount).IsRequired().HasColumnType("int(3)");
                entity.Property(h => h.total).IsRequired().HasColumnType("double(18,2)");
                entity.Property(h => h.status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasConversion<StatusValueConverter>();
            });

            //Configuration of the entity Record
            builder.Entity<Record>().HasKey(r => r.Id);
            builder.Entity<Record>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Record>().Property(r => r.customer_id).IsRequired();
            builder.Entity<Record>().Property(r => r.type).IsRequired().HasMaxLength(100);
            builder.Entity<Record>().Property(r => r.year).IsRequired();
            builder.Entity<Record>().Property(r => r.product).IsRequired().HasMaxLength(100);
            builder.Entity<Record>().Property(r => r.batch).IsRequired().HasColumnType("int(3)");
            builder.Entity<Record>().Property(r => r.stock).IsRequired().HasColumnType("int(3)");

            //Configuration of the entity Record
            builder.UseSnakeCaseNamingConvention();
        }
    }
}
