

using Corebyte_platform.Replenishment.Domain.Model.Queries;

namespace Corebyte_platform.Replenishment.Domain.Services;

/// <summary>
///  Order requests query service interface
/// </summary>

public interface IReplenishmentQueryService
{   
    /// <summary>
    ///  Handle get all orders requests query
    /// </summary>
    /// <param name="requestsQuery"></param>
    /// <returns>
    /// The order requests if successful otherwise null
    /// </returns>
    
    Task<IEnumerable<Model.Aggregate.Replenishment>> Handle(GetAllReplenishmentQuery requestsQuery);
    
    Task<Model.Aggregate.Replenishment?> Handle(GetReplenishmentByIdQuery query);
}