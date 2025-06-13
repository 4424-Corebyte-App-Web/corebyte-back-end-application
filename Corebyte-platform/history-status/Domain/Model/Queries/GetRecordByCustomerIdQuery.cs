using Corebyte_platform.history_status.Domain.Model.ValueObjects;

namespace Corebyte_platform.history_status.Domain.Model.Queries
{
    /// <summary>
    /// Query to get a record by customerId.
    /// </summary>
    /// <param name="customerId">The customerId to search</param>
    public record GetRecordByCustomerIdQuery(CustomerId customerId);
}