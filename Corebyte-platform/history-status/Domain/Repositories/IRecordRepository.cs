using Corebyte_platform.history_status.Domain.Model.Aggregates;
using Corebyte_platform.history_status.Domain.Model.Commands;
using Corebyte_platform.history_status.Domain.Model.ValueObjects;
using Corebyte_platform.Shared.Domain.Repositories;

namespace Corebyte_platform.history_status.Domain.Repositories
{
    public interface IRecordRepository: IBaseRepository<Record>
    {
        
        /// <summary>
        /// Finds all history records for a specific customerId
        /// </summary>
        /// <param name="customerId">customer Id to search for</param>
        /// <returns>Collection of records</returns>
        Task<IEnumerable<Record>> findByCustomerIdAsync(CustomerId customerId);

        Task<Record?> findByStockAsync(int stock);

        Task<Record?> findByTypeAndProductAsync(string type,string product);

        /// <summary>
        /// Search for an existing record with the same data
        /// </summary>
        /// <param name="command">Command with the record data to search for</param>
        /// <returns>The existing record if found, null otherwise</returns>
        Task<Record?> FindRecordByDetailsAsync(CreateRecordCommand command);

        /// <summary>
        /// Deletes all records for a specific id
        /// </summary>
        /// <param name="id">Id name to delete records for</param>
        /// <returns>Number of records deleted</returns>
        Task<int> DeleteRecordByIdAsync(int id);

        /// <summary>
        /// Finds a record with the same customerId, type and product but different ID
        /// </summary>
        /// <param name="id">ID to exclude from search</param>
        /// <param name="customerId">Customer Id name to search for</param>
        /// <param name="type">Type name to search for</param>
        /// <param name="product">Product to search for</param>
        /// <returns>Matching record if found, null otherwise</returns>
        Task<Record?> FindRecordByDetailsExceptIdAsync(int id, CustomerId customerId, string type, string product);

        /// <summary>
        /// Updates an existing record
        /// </summary>
        /// <param name="record">The record to update</param>
        /// <returns>Task representing the asynchronous operation</returns>
        Task UpdateAsync(Record record);
    }
}
