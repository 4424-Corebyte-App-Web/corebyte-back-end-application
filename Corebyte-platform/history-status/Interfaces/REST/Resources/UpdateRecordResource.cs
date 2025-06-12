namespace Corebyte_platform.history_status.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource for updating a record
    /// </summary>
    /// <param name="CustomerId">Customer id</param>
    /// <param name="Type">Record type</param>
    /// <param name="Year">Record year</param>
    /// <param name="Product">Product name</param>
    /// <param name="Batch">Batch number</param>
    /// <param name="Stock">Stock quantity</param>
    public record UpdateRecordResource(
        int CustomerId, string Type, DateTime Year, string Product, int Batch, int Stock
        );
}
