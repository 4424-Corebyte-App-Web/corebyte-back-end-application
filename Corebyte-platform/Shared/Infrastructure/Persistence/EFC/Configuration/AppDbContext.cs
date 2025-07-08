
   using Corebyte_platform.Replenishment.Domain.Model.Aggregate;
   using replenishment.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
   using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
   using Microsoft.EntityFrameworkCore;


   namespace replenishment.API.Shared.Infrastructure.Persistence.EFC.Configuration
   {
      public class AppDbContext(DbContextOptions options) : DbContext(options)
      {
         protected override void OnConfiguring(DbContextOptionsBuilder builder)
         {
            //Para campos de auditor (CreatedDate, UpdatedDate)
            builder.AddCreatedUpdatedInterceptor();
            base.OnConfiguring(builder);
         }
      
         protected override void OnModelCreating(ModelBuilder builder)
         {
            base.OnModelCreating(builder);
         
         
            //=================================================================================================
            //||                                    CONFIGURATION OF THE TABLES                              ||                              
            //=================================================================================================

            // Oscar
            builder.Entity<Replenishment>().HasKey(b=> b.Id);
            builder.Entity<Replenishment>().Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Entity<Replenishment>().Property(b => b.OrderNumber);
            builder.Entity<Replenishment>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            builder.Entity<Replenishment>().Property(b => b.Type).IsRequired().HasMaxLength(50);
            builder.Entity<Replenishment>().Property(b => b.Date).IsRequired();
            builder.Entity<Replenishment>().Property(b => b.StockActual).IsRequired();
            builder.Entity<Replenishment>().Property(b => b.StockMinimo).IsRequired();
            builder.Entity<Replenishment>().Property(b => b.Price).IsRequired().HasColumnType("decimal(18,2)");
         
         
            builder.UseSnakeCaseNamingConvention();
         }
      }
   }