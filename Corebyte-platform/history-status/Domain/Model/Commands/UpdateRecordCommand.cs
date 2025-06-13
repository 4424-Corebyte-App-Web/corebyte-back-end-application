using Corebyte_platform.history_status.Domain.Model.ValueObjects;

namespace Corebyte_platform.history_status.Domain.Model.Commands
{
    /// <summary>
    /// Command to update an existing record
    /// </summary>
    /// <param name="id">The ID of the record to update</param>
    /// <param name="CustomerId">CustomerId</param>
    /// <param name="Type">Type</param>
    /// <param name="Year">Year</param>
    /// <param name="Product">Product</param>
    /// <param name="Batch">Batch</param>
    /// <param name="Stock">Transaction Stock</param>
    public record UpdateRecordCommand(int id, CustomerId CustomerId, string Type, DateTime Year, string Product, int Batch, int Stock);
}