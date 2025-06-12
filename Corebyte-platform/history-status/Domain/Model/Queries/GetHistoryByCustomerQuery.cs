namespace Corebyte_platform.history_status.Domain.Model.Queries
{
    /// <summary>
    ///     Query to get a history by customer.
    /// </summary>
    /// <param name="customer">The customer to search</param>
    public record GetHistoryByCustomerQuery(string customer);
}
