using Corebyte_platform.Replenishment.Domain.Model.ValueObjects;

namespace Corebyte_platform.Replenishment.Domain.Model.Commands;

public record CreateReplenishmentCommand( 
    string Name,
    string Type,
    string Date,
    int StockActual,
    int StockMinimo,
    decimal Price 
    );