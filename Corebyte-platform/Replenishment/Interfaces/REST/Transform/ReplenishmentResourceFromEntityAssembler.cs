using Corebyte_platform.Replenishment.Interfaces.REST.Resources;

namespace Corebyte_platform.Replenishment.Interfaces.REST.Transform;

public static class ReplenishmentResourceFromEntityAssembler
{
    public static ReplenishmentResource ToResourceFromEntity(Domain.Model.Aggregate.Replenishment entity)
    {
        return new ReplenishmentResource
        (
            entity.Id,
            entity.Name,
            entity.Type,
            entity.Date,
            entity.StockActual,
            entity.StockMinimo,
            entity.Price
        );
    }
    
}