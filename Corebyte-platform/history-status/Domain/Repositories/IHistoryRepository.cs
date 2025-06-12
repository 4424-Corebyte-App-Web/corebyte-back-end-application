using Corebyte_platform.history_status.Domain.Model.Aggregates;
using Corebyte_platform.history_status.Domain.Model.Commands;
using Corebyte_platform.Shared.Domain.Repositories;

namespace Corebyte_platform.history_status.Domain.Repositories
{
    public interface IHistoryRepository: IBaseRepository<History>
    {
        /// <summary>
        /// Finds all history records for a specific customer
        /// </summary>
        /// <param name="customer">Customer name to search for</param>
        /// <returns>Collection of history records</returns>
        Task<IEnumerable<History>> findByCustomerAsync(string customer);
        
        Task<History?> findByProductAsync(string product);
        Task<History?> findByStatusAsync(string status);
        Task<History?> findByAmountAndTotalAsync(int amount, double total);

        /// <summary>
        /// Search for an existing record with the same data
        /// </summary>
        /// <param name="command">Command with the history data to search for</param>
        /// <returns>The existing record if found, null otherwise</returns>
        Task<History?> FindByDetailsAsync(CreateHistoryCommand command);
        
        /// <summary>
        /// Deletes all history records for a specific id
        /// </summary>
        /// <param name="id">Id name to delete records for</param>
        /// <returns>Number of records deleted</returns>
        Task<int> DeleteByIdAsync(int id);

        /// <summary>
        /// Finds a history record with the same customer, product and date but different ID
        /// </summary>
        /// <param name="id">ID to exclude from search</param>
        /// <param name="customer">Customer name to search for</param>
        /// <param name="product">Product name to search for</param>
        /// <param name="date">Date to search for</param>
        /// <returns>Matching history record if found, null otherwise</returns>
        Task<History?> FindByDetailsExceptIdAsync(int id, string customer, string product, DateTime date);

        /// <summary>
        /// Updates an existing history record
        /// </summary>
        /// <param name="history">The history record to update</param>
        /// <returns>Task representing the asynchronous operation</returns>
        Task UpdateAsync(History history);
    }
}
