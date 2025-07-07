using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Corebyte_platform.batch_management.Domain.Model.Aggregates;
using Corebyte_platform.batch_management.Domain.Repositories;

namespace Corebyte_platform.batch_management.Infrastucture.Repositories
{
    public class BatchRepository : IBatchRepository
    {
        private readonly BatchContext _context;

        public BatchRepository(BatchContext context)
        {
            _context = context;
        }

        public async Task<Batch?> GetByIdAsync(Guid id)
        {
            return await _context.Batches.FindAsync(id);
        }

        public async Task<IEnumerable<Batch>> ListAsync()
        {
            return await _context.Batches.ToListAsync();
        }

        public async Task AddAsync(Batch batch)
        {
            await _context.Batches.AddAsync(batch);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Batch batch)
        {
            _context.Batches.Update(batch);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Batches.FindAsync(id);
            if (entity != null)
            {
                _context.Batches.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

