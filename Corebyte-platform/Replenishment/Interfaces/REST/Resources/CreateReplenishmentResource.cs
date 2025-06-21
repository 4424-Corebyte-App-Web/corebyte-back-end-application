namespace Corebyte_platform.Replenishment.Interfaces.REST.Resources;

/// <summary>
///  Resource to create in replenishment
/// </summary>
/*
 * string OrderNumber,
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
    string OrderNumber,
    string Name,
    string Type,
    string Date,
    int StockActual,
    int StockMinimo,
    decimal Price
    
    );