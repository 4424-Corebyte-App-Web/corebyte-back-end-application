using EntityFrameworkCore;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Configuration.Extensions;
using Corebyte_platform.history_status.Domain.Model.Aggregates;
using Corebyte_platform.orders.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Corebyte_platform.batch_management.Domain.Model.Aggregates;



namespace Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Configuration
{
    /// <summary>
    ///     Application database context
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            // Add the created and updated interceptor
            builder.AddCreatedUpdatedInterceptor();
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Configuration of the entity History
            builder.Entity<History>().HasKey(h => h.Id);
            builder.Entity<History>().Property(h => h.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<History>().Property(h => h.customer).IsRequired().HasMaxLength(100);
            builder.Entity<History>().Property(h => h.date).IsRequired();
            builder.Entity<History>().Property(h => h.product).IsRequired().HasMaxLength(100);
            builder.Entity<History>().Property(h => h.amount).IsRequired().HasColumnType("int(3)");
            builder.Entity<History>().Property(h => h.total).IsRequired().HasColumnType("double(18,2)");
            builder.Entity<History>().Property(h => h.status).IsRequired().HasMaxLength(50);

            //Configuration of the entity Record
            builder.Entity<Record>().HasKey(r => r.Id);
            builder.Entity<Record>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Record>().Property(r => r.customer_id).IsRequired();
            builder.Entity<Record>().Property(r => r.type).IsRequired().HasMaxLength(100);
            builder.Entity<Record>().Property(r => r.year).IsRequired();
            builder.Entity<Record>().Property(r => r.product).IsRequired().HasMaxLength(100);
            builder.Entity<Record>().Property(r => r.batch).IsRequired().HasColumnType("int(3)");
            builder.Entity<Record>().Property(r => r.stock).IsRequired().HasColumnType("int(3)");
            
            // Configuration of the Order entity
            builder.Entity<Order>().HasKey(o => o.Id);
            builder.Entity<Order>().Property(o => o.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Order>().Property(o => o.Customer).IsRequired().HasMaxLength(100);
            builder.Entity<Order>().Property(o => o.Date).IsRequired();
            builder.Entity<Order>().Property(o => o.Product).IsRequired().HasMaxLength(100);
            builder.Entity<Order>().Property(o => o.Amount).IsRequired();
            builder.Entity<Order>().Property(o => o.Total).IsRequired().HasColumnType("decimal(18,2)");

            //Configuration of the entity Batch
            builder.Entity<Batch>().HasKey(b => b.Id);
            builder.Entity<Batch>().Property(b => b.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Batch>().Property(b => b.Name).IsRequired();
            builder.Entity<Batch>().Property(b => b.Type).IsRequired().HasMaxLength(100);
            builder.Entity<Batch>().Property(b => b.Status).IsRequired();
            builder.Entity<Batch>().Property(b => b.Temperature).IsRequired();
            builder.Entity<Batch>().Property(b => b.Amount).IsRequired().HasMaxLength(50);
            builder.Entity<Batch>().Property(b => b.Total).IsRequired().HasColumnType("decimal(18,2)");
            builder.Entity<Batch>().Property(b => b.Date).IsRequired();
            builder.Entity<Batch>().Property(b => b.NLote).IsRequired().HasMaxLength(100);
            
            builder.UseSnakeCaseNamingConvention();
        }
    }
}
