using Corebyte_platform.Replenishment.Domain.Model.Aggregate;
using Corebyte_platform.Replenishment.Domain.Model.Commands;

namespace Corebyte_platform.Replenishment.Domain.Services;

/// <summary>
/// Order requests command service interface
/// </summary>

public interface IReplenishmentCommandService
{   
    /// <summary>
    ///  Handle create order requests command
    /// </summary>
    /// <param name="command"></param>
    /// <returns>
    /// The created order requests if successful otherwise null
    /// </returns>
    public Task<Model.Aggregate.Replenishment?> Handle(CreateReplenishmentCommand command);
    public Task<Model.Aggregate.Replenishment?> Handle(DeleteReplenishmentCommand command);
    public Task<Model.Aggregate.Replenishment?> Handle(UpdateReplenishmentByIdCommand command);
    
}
