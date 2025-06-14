using System.Collections.Generic;
using System.Threading.Tasks;
using Corebyte_platform.batch_management.Domain.Common.Repositories;
using Corebyte_platform.batch_management.Domain.Model.Aggregates;
using Corebyte_platform.batch_management.Domain.Model.Queries;

namespace Corebyte_platform.batch_management.Domain.Repositories
{
    /// <summary>
    /// Repository interface for Batch entity operations
    /// </summary>
    public interface IBatchRepository : IRepositoryBase<Batch>
    {
        /// <summary>
        /// Get batches by filter criteria
        /// </summary>
        /// <param name="filter">Filter parameters</param>
        /// <returns>Filtered collection of batches</returns>
        Task<IEnumerable<Batch>> GetByFilterAsync(GetBatchesQuery filter);
        
        /// <summary>
        /// Get batches by lot number
        /// </summary>
        /// <param name="nlote">The lot number</param>
        /// <returns>Collection of batches with the specified lot number</returns>
        Task<IEnumerable<Batch>> GetByLotAsync(string nlote);
    }
}

