namespace Corebyte_platform.history_status.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the data provided by the server about a history. 
    /// </summary>
    /// <param name="Id">The server-generated ID</param>
    /// <param name="customer">The customer</param>
    /// <param name="date">The date</param>
    /// <param name="product">The product</param>
    /// <param name="amount">The amount</param>
    /// <param name="total">The total</param>
    /// <param name="status">The status</param>
    public record HistoryResource(int Id, string customer, DateTime date, string product, int amount, double total, string status);
}