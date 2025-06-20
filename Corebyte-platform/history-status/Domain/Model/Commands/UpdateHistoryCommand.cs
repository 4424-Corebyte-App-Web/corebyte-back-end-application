namespace Corebyte_platform.history_status.Domain.Model.Commands
{
    /// <summary>
    /// Command to update an existing history record
    /// </summary>
    /// <param name="Id">The ID of the history record to update</param>
    /// <param name="Customer">Customer name</param>
    /// <param name="Date">Transaction date</param>
    /// <param name="Product">Product name</param>
    /// <param name="Amount">Product quantity</param>
    /// <param name="Total">Total amount</param>
    /// <param name="Status">Transaction status</param>
    public record UpdateHistoryCommand(
        int Id,
        string Customer,
        DateTime Date,
        string Product,
        int Amount,
        double Total,
        string Status);
}