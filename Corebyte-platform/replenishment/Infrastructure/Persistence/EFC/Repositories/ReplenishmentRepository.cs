using Corebyte_platform.replenishment.Domain.Respositories;
using Corebyte_platform.Shared.Infrastructure.Persistence.EFC.Repositories;
using Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Corebyte_platform.replenishment.Infrastructure.Persistence.EFC.Repositories;

public class ReplenishmentRepository(AppDbContext context)
    : BaseRepository<Domain.Model.Aggregate.Replenishment>(context), IReplenishmentRepository
{
    public async Task<Domain.Model.Aggregate.Replenishment?> FindByOrderNumberAsync(string orderNumber)
    {
        return await Context.Set<Domain.Model.Aggregate.Replenishment>().FirstOrDefaultAsync(replenishment => replenishment.OrderNumber == orderNumber);
    }
}