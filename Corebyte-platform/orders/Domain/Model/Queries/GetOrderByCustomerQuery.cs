namespace Corebyte_platform.orders.Domain.Model.Queries
{
    /// <summary>
    /// Query to get a order customer.
    /// </summary>
    /// <param name="customer">The customer to search</param>
    /// 
    public record GetOrderByCustomerQuery(string customer);
    
    
}
