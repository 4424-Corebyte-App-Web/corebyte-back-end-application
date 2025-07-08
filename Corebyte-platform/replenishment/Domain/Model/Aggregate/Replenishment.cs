using Corebyte_platform.replenishment.Domain.Model.Commands;

namespace Corebyte_platform.replenishment.Domain.Model.Aggregate;

public class Replenishment
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Type { get;private set; }
    public string Date { get;private set; }
    public string OrderNumber { get; private set; }
    public int StockActual { get;private set; }
    public int StockMinimo { get;private set; }
    public decimal Price { get;private set; }


    public Replenishment()
    {
        this.Id = 0;
        this.Name = string.Empty;
        this.Type = string.Empty;
        this.Date = string.Empty;
        this.Price = 0;
        this.OrderNumber = "LIMA-0000";
        this.StockActual = 0;
        this.StockMinimo = 0;
        
    }

    
    
    public Replenishment(CreateReplenishmentCommand requestsCommand): this()
    {
        Name = requestsCommand.Name;
        Type = requestsCommand.Type;
        Date = requestsCommand.Date;
        StockActual = requestsCommand.StockActual;
        StockMinimo = requestsCommand.StockMinimo;
        Price = requestsCommand.Price;
    }

    public Replenishment(UpdateReplenishmentByIdCommand command)
    {
        this.Date = command.Date;
        this.Name = command.Name;
        this.Type = command.Type;
        this.OrderNumber = command.OrderNumber;
        this.StockActual = command.StockActual;
        this.StockMinimo = command.StockMinimo;
        this.Price = command.Price;
    }
}