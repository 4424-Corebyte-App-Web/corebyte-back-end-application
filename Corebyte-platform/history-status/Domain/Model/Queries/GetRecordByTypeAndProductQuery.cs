namespace Corebyte_platform.history_status.Domain.Model.Queries
{
    /// <summary>
    ///     Query to get a record by type and product.
    /// </summary>
    /// <param name="type">The type to search</param>
    /// <param name="product">The product to search</param>
    public record GetRecordByTypeAndProductQuery(string type, string product);
}
