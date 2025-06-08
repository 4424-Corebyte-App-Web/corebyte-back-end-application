using EntityFrameworkCore;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using CatchUp.Shared.Infrastucture.Persistence.EFC.Configuration.Extensions;
using CatchUp.News.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace CatchUp.Shared.Infrastucture.Persistence.EFC.Configuration
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {   builder.AddCreatedUpdatedInterceptor();
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //cambiar con la su propia entidad
            //builder.Entity<FavoriteSource>().HasKey(f => f.Id);
            //builder.Entity<FavoriteSource>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
            //builder.Entity<FavoriteSource>().Property(f => f.SourceId).IsRequired();
            //builder.Entity<FavoriteSource>().Property(f => f.NewsApiKey).IsRequired();

            builder.UseSnakeCaseNamingConvention();
        }
    }
}
