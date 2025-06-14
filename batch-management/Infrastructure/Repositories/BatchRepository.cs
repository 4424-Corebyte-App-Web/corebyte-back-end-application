using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Corebyte_platform.batch_management.Domain.Model.Aggregates;
using Corebyte_platform.batch_management.Domain.Model.Queries;
using Corebyte_platform.batch_management.Domain.Repositories;
using Corebyte_platform.batch_management.Infrastructure.Data;

namespace Corebyte_platform.batch_management.Infrastructure.Repositories
{
    /// <summary>
    /// Implementation of the IBatchRepository interface using Entity Framework Core
    /// </summary>
    public class BatchRepository : RepositoryBase<Batch>, IBatchRepository
    {
        private readonly ApplicationDbContext _dbContext;
        
        /// <summary>
        /// Constructor for BatchRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public BatchRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }
        
        /// <inheritdoc/>
        public async Task<IEnumerable<Batch>> GetByFilterAsync(GetBatchesQuery filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));
                
            var query = _dbContext.Batches.AsQueryable();
            
            if (filter.FromDate.HasValue)
                query = query.Where(b => b.Date >= filter.FromDate);
                
            if (filter.ToDate.HasValue)
                query = query.Where(b => b.Date <= filter.ToDate);
                
            if (!string.IsNullOrEmpty(filter.Type))
                query = query.Where(b => b.Type == filter.Type);
                
            if (!string.IsNullOrEmpty(filter.Status))
                query = query.Where(b => b.Status == filter.Status);
                
            return await query.ToListAsync();
        }
        
        /// <inheritdoc/>
        public async Task<IEnumerable<Batch>> GetByLotAsync(string nlote)
        {
            if (string.IsNullOrEmpty(nlote))
                throw new ArgumentException("Lot number cannot be null or empty", nameof(nlote));
                
            return await _dbContext.Batches
                .Where(b => b.NLote == nlote)
                .ToListAsync();
        }
    }
}

