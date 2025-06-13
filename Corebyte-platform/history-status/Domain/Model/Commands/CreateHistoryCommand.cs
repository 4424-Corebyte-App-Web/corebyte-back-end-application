namespace Corebyte_platform.history_status.Domain.Model.Commands
{
    /// <summary>
    ///     Command to create a history entry.
    /// </summary>
    /// <param name="customer">The customer obtained from provider</param>
    /// <param name="date">The date of the history</param>
    /// <param name="product">The product of the history</param>
    /// <param name="amount">The amount of the history</param>
    /// <param name="total">The total of the history</param>
    /// <param name="status">The status of the history</param>
    public record CreateHistoryCommand(string customer, DateTime date, string product, int amount, double total, string status);
}