using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Corebyte_platform.batch_management.Domain.Model.Aggregates;
using Corebyte_platform.batch_management.Domain.Model.Queries;
using Corebyte_platform.batch_management.Domain.Repositories;
using Corebyte_platform.batch_management.Domain.Common.Guards;

namespace Corebyte_platform.batch_management.Application.Infernal.QueryServices
{
    /// <summary>
    /// Interface for batch query operations
    /// </summary>
    public interface IBatchQueryService
    {
        /// <summary>
        /// Gets a batch by ID
        /// </summary>
        /// <param name="query">The get batch query</param>
        /// <returns>The batch if found, null otherwise</returns>
        Task<Batch> GetBatchAsync(GetBatchQuery query);
        
        /// <summary>
        /// Gets batches by filter criteria
        /// </summary>
        /// <param name="query">The get batches query with filter parameters</param>
        /// <returns>Filtered collection of batches</returns>
        Task<IEnumerable<Batch>> GetBatchesAsync(GetBatchesQuery query);
        
        /// <summary>
        /// Gets batches by lot number
        /// </summary>
        /// <param name="query">The get batches by lot query</param>
        /// <returns>Collection of batches with the specified lot number</returns>
        Task<IEnumerable<Batch>> GetBatchesByLotAsync(GetBatchesByLotQuery query);
    }
    
    /// <summary>
    /// Implementation of batch query operations
    /// </summary>
    public class BatchQueryService : IBatchQueryService
    {
        private readonly IBatchRepository _repository;
        
        /// <summary>
        /// Constructor for BatchQueryService
        /// </summary>
        /// <param name="repository">The batch repository</param>
        public BatchQueryService(IBatchRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        
        /// <inheritdoc/>
        public async Task<Batch> GetBatchAsync(GetBatchQuery query)
        {
            Guard.ThrowIfNull(query, nameof(query));
                
            return await _repository.GetByIdAsync(query.Id);
        }
        
        /// <inheritdoc/>
        public async Task<IEnumerable<Batch>> GetBatchesAsync(GetBatchesQuery query)
        {
            Guard.ThrowIfNull(query, nameof(query));
                
            return await _repository.GetByFilterAsync(query);
        }
        
        /// <inheritdoc/>
        public async Task<IEnumerable<Batch>> GetBatchesByLotAsync(GetBatchesByLotQuery query)
        {
            Guard.ThrowIfNull(query, nameof(query));
                
            return await _repository.GetByLotAsync(query.NLote);
        }
    }
}

