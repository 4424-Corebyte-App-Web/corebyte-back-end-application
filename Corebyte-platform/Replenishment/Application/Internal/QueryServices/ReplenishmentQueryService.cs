using Corebyte_platform.Replenishment.Domain.Model.Queries;
using Corebyte_platform.Replenishment.Domain.Respositories;
using Corebyte_platform.Replenishment.Domain.Services;

namespace Corebyte_platform.Replenishment.Application.Internal.QueryServices;

public class ReplenishmentQueryService(IReplenishmentRepository repository): IReplenishmentQueryService

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

