namespace Corebyte_platform.orders.Domain.Model.Queries
{

    /// <summary>
    /// Query to get an order by amount and total.
    /// </summary>
    /// <param name="amount">The amount to seacrh</param>
    /// <param name="total">The total ID to search</param>
    /// 
    public record GetOrderByAmountAndTotalQuery(int amount, double total);
    
    
}
