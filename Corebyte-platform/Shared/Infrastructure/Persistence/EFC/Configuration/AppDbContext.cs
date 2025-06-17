using EntityFrameworkCore;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Configuration.Extensions;
//using CatchUp.News.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Configuration
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.AddCreatedUpdatedInterceptor();
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<FavoriteSource>().HasKey(f => f.Id);
            //builder.Entity<FavoriteSource>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
            //builder.Entity<FavoriteSource>().Property(f => f.SourceId).IsRequired();
            //builder.Entity<FavoriteSource>().Property(f => f.NewsApiKey).IsRequired();

            builder.UseSnakeCaseNamingConvention();
        }
    }
}
