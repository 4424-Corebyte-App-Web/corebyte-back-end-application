namespace Corebyte_platform.history_status.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the data provided by the server about a record. 
    /// </summary>
    /// <param name="Id">The server-generated ID</param>
    /// <param name="customerId">The customer id</param>
    /// <param name="type">The type</param>
    /// <param name="year">The year</param>
    /// <param name="product">The product</param>
    /// <param name="batch">The batch</param>
    /// <param name="stock">The stock</param>
    public record RecordResource(int Id, int customerId, string type, DateTime year, string product, int batch, int stock);
}
