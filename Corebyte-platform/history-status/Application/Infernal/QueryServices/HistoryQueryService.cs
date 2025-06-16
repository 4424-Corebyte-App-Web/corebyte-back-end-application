using Corebyte_platform.history_status.Domain.Model.Aggregates;
using Corebyte_platform.history_status.Domain.Model.Queries;
using Corebyte_platform.history_status.Domain.Repositories;
using Corebyte_platform.history_status.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Corebyte_platform.history_status.Application.Infernal.QueryServices
{
    ///<summary>
    /// Query service for History aggregate
    ///</summary>
    /// <param name="historyRepository">The history repository</param>
    public class HistoryQueryService(IHistoryRepository historyRepository): IHistoryQueryService
    {
        /// <summary>
        ///     Gets all histories.
        /// </summary>
        /// <param name="query">The GetAllHistoriesQuery query</param>
        /// <returns>A list of histories</returns>
        public async Task<IEnumerable<History>> Handle(GetAllHistoriesQuery query)
        {
            return await historyRepository.ListAsync();
        }

        /// <summary>
        ///     Gets histories by customer.
        /// </summary>
        /// <param name="query">The GetHistoryByCustomerQuery query</param>
        /// <returns>A list of histories</returns>
        public async Task<IEnumerable<History>>Handle(GetHistoryByCustomerQuery query)
        {
            return await historyRepository.findByCustomerAsync(query.customer);
        }

        /// <summary>
        ///     Gets histories by product.
        /// </summary>
        /// <param name="query">The GetHistoryByProductQuery query</param>
        /// <returns>A list of histories</returns>
        public async Task<History?> Handle(GetHistoryByProductQuery query)
        {
            return await historyRepository.findByProductAsync(query.product);
        }

        /// <summary>
        ///     Gets histories by status.
        /// </summary>
        /// <param name="query">The GetHistoryByStatusQuery query</param>
        /// <returns>A list of histories</returns>
        public async Task<History?> Handle(GetHistoryByStatusQuery query)
        {
            return await historyRepository.findByStatusAsync(query.status);
        }

        /// <summary>
        ///     Gets histories by amount and total.
        /// </summary>
        /// <param name="query">The GetHistoryByAmountAndTotalQuery query</param>
        /// <returns>A list of histories</returns>
        public async Task<History?> Handle(GetHistoryByAmountAndTotalQuery query)
        {
            return await historyRepository.findByAmountAndTotalAsync(query.amount, query.total);
        }

        /// <summary>
        ///     Gets histories by ID.
        /// </summary>
        /// <param name="query">The GetHistoryByIdQuery query</param>
        /// <returns>A list of histories</returns>
        public async Task<IEnumerable<History>> Handle(GetHistoryByIdQuery query)
        {
            var history = await historyRepository.FindByIdAsync(query.Id);
            return history != null ? new List<History> { history } : Enumerable.Empty<History>();
        }
        
    }
}
