namespace Corebyte_platform.Replenishment.Interfaces.REST.Resources;

public record ReplenishmentResource(
    int Id,
    string OrderNumber,
    string Name,
    string Type,
    string Date,
    int StockActual,
    int StockMinimo,
    decimal Price

    
    );