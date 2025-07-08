namespace Corebyte_platform.Replenishment.Interfaces.REST.Resources;

/// <summary>
///  Resource to create in replenishment
/// </summary>
/*
    string Name,
    string Type,
    string Date,
    int StockActual,
    int StockMinimo,
    string Price
 */
/// 
/// 
public record CreateReplenishmentResource(
    string Name,
    string Type,
    string Date,
    int StockActual,
    int StockMinimo,
    decimal Price
    
);