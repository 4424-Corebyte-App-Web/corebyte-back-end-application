namespace Corebyte_platform.Replenishment.Interfaces.REST.Resources;

public record UpdateReplenishmentByIdResource(
    string OrderNumber,
    string Name,
    string Type,
    string Date,
    int StockActual,
    int StockMinimo,
    decimal Price

    );