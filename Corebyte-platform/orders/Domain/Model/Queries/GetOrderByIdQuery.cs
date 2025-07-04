namespace Corebyte_platform.orders.Domain.Model.Queries
{
    /// <summary>
    /// Query to get a orders by id.
    /// </summary>
    /// <param name="Id">The id to search</param>
    /// 
    public record GetOrderByIdQuery(int Id);
    
    
}
