using Corebyte_platform.Shared.Domain.Repositories;

namespace Corebyte_platform.replenishment.Domain.Respositories;

public interface IReplenishmentRepository : IBaseRepository<Model.Aggregate.Replenishment>
{
    Task<Model.Aggregate.Replenishment?> FindByOrderNumberAsync(string orderNumber);
    
}