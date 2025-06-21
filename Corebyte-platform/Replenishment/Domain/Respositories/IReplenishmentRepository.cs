using replenishment.Shared.Domain.Repositories;

namespace Corebyte_platform.Replenishment.Domain.Respositories;

public interface IReplenishmentRepository : IBaseRepository<Model.Aggregate.Replenishment>
{
    Task<Model.Aggregate.Replenishment?> FindByOrderNumberAsync(string orderNumber);
    
}
