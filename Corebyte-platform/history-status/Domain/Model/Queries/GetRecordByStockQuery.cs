namespace Corebyte_platform.history_status.Domain.Model.Queries
{
    /// <summary>
    ///     Query to get a record by stock.
    /// </summary>
    /// <param name="stock">The stock to search</param>
    public record GetRecordByStockQuery(int stock);
}
