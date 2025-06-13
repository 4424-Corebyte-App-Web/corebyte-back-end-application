using Corebyte_platform.history_status.Domain.Model.Aggregates;
using Corebyte_platform.history_status.Domain.Model.Queries;

namespace Corebyte_platform.history_status.Domain.Services
{
    public interface IRecordQueryService
    {
        
        /// <summary>
        /// Handle the GetAllRecodQuery.
        /// </summary>
        /// <returns>An IEnumerable containing all Record objects</returns>
        Task<IEnumerable<Record>> Handle(GetAllRecordQuery query);

        /// <summary>
        ///     Handle the GetRecordByIdQuery.
        /// </summary>
        /// <remarks>
        ///     This method handles the GetRecordByIdQuery. It returns all the recod for the given
        ///     id.
        /// </remarks>
        /// <param name="query">The GetRecordByIdQuery query</param>
        /// <returns>An IEnumerable containing the Record objects</returns>
        Task<IEnumerable<Record>> Handle(GetRecordByIdQuery query);

        /// <summary>
        ///     Handle the GetRecordByCustomerIdQuery.
        /// </summary>
        /// <remarks>
        ///     This method handles the GetRecordByCustomerIdQuery. It returns all the recod for the given
        ///     customerId.
        /// </remarks>
        /// <param name="query">The GetRecordByCustomerIdQuery query</param>
        /// <returns>An IEnumerable containing the Record objects</returns>
        Task<IEnumerable<Record>> Handle(GetRecordByCustomerIdQuery query);
        /// <summary>
        ///     Handle the GetRecordByStockQuery.
        /// </summary>
        /// <remarks>
        ///     This method handles the GetRecordByStockQuery. It returns all the recod for the given
        ///     stock.
        /// </remarks>
        /// <param name="query">The GetRecordByStockQuery query</param>
        /// <returns>An IEnumerable containing the Record objects</returns>
        Task<IEnumerable<Record>> Handle(GetRecordByStockQuery query);
        /// <summary>
        ///     Handle the GetRecordByTypeAndProductQuery.
        /// </summary>
        /// <remarks>
        ///     This method handles the GetRecordByTypeAndProductQuery. It returns all the recod for the given
        ///     type and product.
        /// </remarks>
        /// <param name="query">The GetRecordByTypeAndProductQuery query</param>
        /// <returns>An IEnumerable containing the Record objects</returns>
        Task<IEnumerable<Record>> Handle(GetRecordByTypeAndProductQuery query);


    }
}
