using Corebyte_platform.replenishment.Domain.Model.ValueObjects;
namespace Corebyte_platform.replenishment.Domain.Model.Commands;

public record CreateReplenishmentCommand( 
    string Name,
    string Type,
    string Date,
    int StockActual,
    int StockMinimo,
    decimal Price 
);