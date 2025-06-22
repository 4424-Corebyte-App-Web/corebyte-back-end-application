using Corebyte_platform.history_status.Domain.Model.Aggregates;
using Corebyte_platform.history_status.Domain.Model.Queries;

namespace Corebyte_platform.history_status.Domain.Services
{
    public interface IHistoryQueryService
    {
        /// <summary>
        /// Handle the GetAllHistoriesQuery.
        /// </summary>
        /// <returns>An IEnumerable containing all History objects</returns>
        Task<IEnumerable<History>> Handle(GetAllHistoriesQuery query);

        Task<IEnumerable<History>> Handle(GetHistoryByIdQuery query);
        /// <summary>
        ///     Handle the GetHistoryByCustomerQuery.
        /// </summary>
        /// <remarks>
        ///     This method handles the GetHistoryByCustomerQuery. It returns all the history for the given
        ///     customer.
        /// </remarks>
        /// <param name="query">The GetHistoryByCustomerQuery query</param>
        /// <returns>An IEnumerable containing the History objects</returns>
        Task<IEnumerable<History> >Handle(GetHistoryByCustomerQuery query);

        /// <summary>
        ///     Handle the GetHistoryByProductQuery.
        /// </summary>
        /// <remarks>
        ///     This method handles the GetHistoryByProductQuery. It returns the history for the given
        ///     product.
        /// </remarks>
        /// <param name="query">The GetHistoryByProductQuery query</param>
        /// <returns>The History object if found, or null otherwise</returns>
        Task<History?> Handle(GetHistoryByProductQuery query);

        /// <summary>
        ///     Handle the GetHistoryByStatusQuery.
        /// </summary>
        /// <remarks>
        ///     This method handles the GetHistoryByStatusQuery. It returns the history for the given status.
        /// </remarks>
        /// <param name="query">The GetHistoryByStatusQuery query</param>
        /// <returns>
        ///     The History object if found, or null otherwise
        /// </returns>
        Task<History?> Handle(GetHistoryByStatusQuery query);

        /// <summary>
        ///     Handle the GetHistoryByAmountAndTotalQuery.
        /// </summary>
        /// <remarks>
        ///     This method handles the GetHistoryByAmountAndTotalQuery. It returns the history for the given amount and total.
        /// </remarks>
        /// <param name="query">The GetHistoryByAmountAndTotalQuery query</param>
        /// <returns>
        ///     The History object if found, or null otherwise
        /// </returns>
        Task<History?> Handle(GetHistoryByAmountAndTotalQuery query);
    }
}
