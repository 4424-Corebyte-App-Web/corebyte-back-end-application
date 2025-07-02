namespace Corebyte_platform.orders.Domain.Model.Queries
{
    /// <summary>
    /// Query to get a orders b6y products.
    /// </summary>
    /// <param name="products">The products to search</param>   
    /// 
    public record GetOrderByProductQuery(string products);
    
    
}
