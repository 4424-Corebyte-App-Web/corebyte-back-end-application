namespace Corebyte_platform.history_status.Domain.Model.Queries
{
    /// <summary>
    ///     Query to get a history by amount and total.
    /// </summary>
    /// <param name="amount">The amount to search</param>
    /// <param name="total">The total ID to search</param>
    public record GetHistoryByAmountAndTotalQuery(int amount, double total);
}
