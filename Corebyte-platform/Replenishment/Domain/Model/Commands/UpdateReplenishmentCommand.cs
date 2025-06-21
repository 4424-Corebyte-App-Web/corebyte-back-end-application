using Corebyte_platform.Replenishment.Domain.Model.ValueObjects;

namespace Corebyte_platform.Replenishment.Domain.Model.Commands;

public record UpdateReplenishmentByIdCommand(
    int Id,
    string OrderNumber,
    string Name,
    string Type,
    string Date,
    int StockActual,
    int StockMinimo,
    decimal Price
    
    );