using Corebyte_platform.replenishment.Domain.Model.Commands;
using Corebyte_platform.replenishment.Domain.Model.Aggregate;
namespace Corebyte_platform.replenishment.Domain.Services;

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