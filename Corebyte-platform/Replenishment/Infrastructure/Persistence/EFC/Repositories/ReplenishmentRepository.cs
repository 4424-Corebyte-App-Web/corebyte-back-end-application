using Corebyte_platform.Replenishment.Domain.Respositories;
using Microsoft.EntityFrameworkCore;
using replenishment.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using replenishment.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Corebyte_platform.Replenishment.Infrastructure.Persistence.EFC.Repositories;

public class ReplenishmentRepository(AppDbContext context)
    : BaseRepository<Domain.Model.Aggregate.Replenishment>(context), IReplenishmentRepository
{
    public async Task<Domain.Model.Aggregate.Replenishment?> FindByOrderNumberAsync(string orderNumber)
    {
        return await Context.Set<Domain.Model.Aggregate.Replenishment>().FirstOrDefaultAsync(replenishment => replenishment.OrderNumber == orderNumber);
    }
}