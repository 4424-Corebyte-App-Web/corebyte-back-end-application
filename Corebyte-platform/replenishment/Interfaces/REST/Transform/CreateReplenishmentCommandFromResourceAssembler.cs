using Corebyte_platform.replenishment.Domain.Model.Commands;
using Corebyte_platform.Replenishment.Interfaces.REST.Resources;

namespace Corebyte_platform.Replenishment.Interfaces.REST.Transform;

public static class CreateReplenishmentCommandFromResourceAssembler
{
    public static CreateReplenishmentCommand ToCommandFromResource(CreateReplenishmentResource resource)
    {
        return new CreateReplenishmentCommand(
            resource.Name,
            resource.Type,
            resource.Date,
            resource.StockActual,
            resource.StockMinimo,
            resource.Price
        );
    }
}