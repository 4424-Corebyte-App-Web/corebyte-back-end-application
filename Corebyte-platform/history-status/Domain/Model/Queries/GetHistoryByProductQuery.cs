namespace Corebyte_platform.history_status.Domain.Model.Queries
{
    /// <summary>
    ///     Query to get a history by product.
    /// </summary>
    /// <param name="product">The product to search</param>
    public record GetHistoryByProductQuery(string product);
}
