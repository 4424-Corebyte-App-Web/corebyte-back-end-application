using Corebyte_platform.history_status.Domain.Model.Aggregates;
using Corebyte_platform.history_status.Domain.Model.Queries;
using Corebyte_platform.history_status.Domain.Repositories;
using Corebyte_platform.history_status.Domain.Services;


namespace Corebyte_platform.history_status.Application.Infernal.QueryServices
{
    ///<summary>
    /// Query service for Record aggregate
    ///</summary>
    /// <param name="recordRepository">The record repository</param>
    public class RecordQueryService(IRecordRepository recordRepository): IRecordQueryService
    {
        /// <summary>
        ///     Gets all records.
        /// </summary>
        /// <param name="query">The GetAllRecordQuery query</param>
        /// <returns>A list of records</returns>
        public async Task<IEnumerable<Record>> Handle(GetAllRecordQuery query)
        {
            return await recordRepository.ListAsync();
        }
        /// <summary>
        ///     Gets records by customer ID.
        /// </summary>
        /// <param name="query">The GetRecordByCustomerIdQuery query</param>
        /// <returns>A list of records</returns>
        public async Task<IEnumerable<Record>> Handle(GetRecordByCustomerIdQuery query)
        {
            return await recordRepository.findByCustomerIdAsync(query.customerId);
        }

        /// <summary>
        ///     Gets records by ID.
        /// </summary>
        /// <param name="query">The GetRecordByIdQuery query</param>
        /// <returns>A list of records</returns>
        public async Task<IEnumerable<Record>> Handle(GetRecordByIdQuery query) { 
            var record = await recordRepository.FindByIdAsync(query.id);
            return record != null ? new List<Record> { record } : Enumerable.Empty<Record>();
        }
        /// <summary>
        ///     Gets records by stock.
        /// </summary>
        /// <param name="query">The GetRecordByStockQuery query</param>
        /// <returns>A list of records</returns>
        public async Task<IEnumerable<Record>> Handle(GetRecordByStockQuery query) { 
            var record = await recordRepository.findByStockAsync(query.stock);
            return record != null ? new List<Record> { record } : Enumerable.Empty<Record>();
        }
        /// <summary>
        ///     Gets records by type and product.
        /// </summary>
        /// <param name="query">The GetRecordByTypeAndProductQuery query</param>
        /// <returns>A list of records</returns>
        public async Task<IEnumerable<Record>> Handle(GetRecordByTypeAndProductQuery query) { 
            var record = await recordRepository.findByTypeAndProductAsync(query.type, query.product);
            return record != null ? new List<Record> { record } : Enumerable.Empty<Record>();
        }
    }
}
