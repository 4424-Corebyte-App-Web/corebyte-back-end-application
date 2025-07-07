namespace Corebyte_platform.orders.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the data provided by the server about an order.
    /// </summary>
    /// <param name="Id">The server-generated ID</param>
    /// <param name="customer">The customer</param>
    /// <param name="date">The date</param>
    /// <param name="product">The product</param>
    /// <param name="amount">The amount</param>
    /// <param name="total">The total</param>
    public record OrderResource(int Id, string customer, DateTime date, string product, int amount, double total, string url);
    
    
}
