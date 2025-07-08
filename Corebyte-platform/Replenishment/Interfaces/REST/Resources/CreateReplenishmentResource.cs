namespace Corebyte_platform.Replenishment.Interfaces.REST.Resources;

/// <summary>
public record CreateReplenishmentResource(
    string Name,
    string Type,
    string Date,
    int StockActual,
    int StockMinimo,
    decimal Price
    
    );