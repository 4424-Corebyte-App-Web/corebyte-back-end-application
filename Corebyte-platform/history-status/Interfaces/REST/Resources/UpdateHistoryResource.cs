namespace Corebyte_platform.history_status.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource for updating a history record
    /// </summary>
    /// <param name="Customer">Customer name</param>
    /// <param name="Date">Transaction date</param>
    /// <param name="Product">Product name</param>
    /// <param name="Amount">Product quantity</param>
    /// <param name="Total">Total amount</param>
    /// <param name="Status">Transaction status</param>
    public record UpdateHistoryResource(
        string Customer,
        DateTime Date,
        string Product,
        int Amount,
        double Total,
        string Status);
}