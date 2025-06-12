using Corebyte_platform.Shared.Domain.Repositories;
using Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Configuration;

namespace Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
