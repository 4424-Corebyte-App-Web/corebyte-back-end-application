using Corebyte_platform.replenishment.Domain.Model.Queries;
using Corebyte_platform.replenishment.Domain.Respositories;
using Corebyte_platform.replenishment.Domain.Services;

namespace Corebyte_platform.replenishment.Application.Internal.QueryServices;

public class ReplenishmentQueryService(IReplenishmentRepository repository):IReplenishmentQueryService
{
    public async Task<IEnumerable<Domain.Model.Aggregate.Replenishment>> Handle(GetAllReplenishmentQuery requestsQuery)
    {
        return await repository.ListAsync();
    }

    public async Task<Domain.Model.Aggregate.Replenishment?> Handle(GetReplenishmentByIdQuery query)
    {
        var replenishment = await repository.FindByIdAsync(query.Id);
        if (replenishment is null)
            throw new Exception("Replenishment not found");
        
        return replenishment;
        
    }
}