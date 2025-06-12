using Corebyte_platform.history_status.Domain.Model.ValueObjects;

namespace Corebyte_platform.history_status.Domain.Model.Commands
{
    /// <summary>
    ///     Command to create a record entry.
    /// </summary>
    /// <param name="customerId">The customerId obtained from provider</param>
    /// <param name="type">The type of the record</param>
    /// <param name="year">The year of the record</param>
    /// <param name="product">The product of the record</param>
    /// <param name="batch">The batch of the record</param>
    /// <param name="stock">The stock of the record</param>
    public record CreateRecordCommand(CustomerId customerId, string type, DateTime year, string product, int batch, int stock);
}
