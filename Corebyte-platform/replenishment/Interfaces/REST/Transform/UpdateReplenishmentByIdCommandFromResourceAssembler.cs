using Corebyte_platform.replenishment.Domain.Model.Commands;
using Corebyte_platform.Replenishment.Interfaces.REST.Resources;

namespace Corebyte_platform.Replenishment.Interfaces.REST.Transform;

public static class UpdateReplenishmentByIdCommandFromResourceAssembler
{
    public static UpdateReplenishmentByIdCommand ToCommandFromResource(int id, UpdateReplenishmentByIdResource resource)
    {
        return new UpdateReplenishmentByIdCommand(
            Id: id,
            OrderNumber: resource.OrderNumber,
            Name: resource.Name,
            Type: resource.Type,
            Date: resource.Date,
            StockActual: resource.StockActual,
            StockMinimo: resource.StockMinimo,
            Price: resource.Price
            

        );
    }
}