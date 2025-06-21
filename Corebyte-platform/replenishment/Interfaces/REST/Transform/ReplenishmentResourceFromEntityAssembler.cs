using Corebyte_platform.Replenishment.Interfaces.REST.Resources;
using Corebyte_platform.replenishment.Domain.Model.Aggregate;

namespace Corebyte_platform.Replenishment.Interfaces.REST.Transform;

public static class ReplenishmentResourceFromEntityAssembler
{
    public static ReplenishmentResource ToResourceFromEntity(replenishment.Domain.Model.Aggregate.Replenishment entity)
    {
        return new ReplenishmentResource
        (
            entity.Id,
            entity.OrderNumber,
            entity.Name,
            entity.Type,
            entity.Date,
            entity.StockActual,
            entity.StockMinimo,
            entity.Price
        );
    }
    
    public static ReplenishmentResource ToResourceFromEntity(CreateReplenishmentResource resource)
    {
        return new ReplenishmentResource
        (
            0, // ID will be set by the database
            resource.OrderNumber,
            resource.Name,
            resource.Type,
            resource.Date,
            resource.StockActual,
            resource.StockMinimo,
            resource.Price
        );
    }
}