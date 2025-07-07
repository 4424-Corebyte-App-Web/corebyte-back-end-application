using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Corebyte_platform.batch_management.Domain.Model.Aggregates;
using Corebyte_platform.batch_management.Domain.Repositories;
using MySql.Data.MySqlClient;

namespace Corebyte_platform.batch_management.Infrastucture.Repositories
{
    public class BatchRepository : IBatchRepository
    {
        private readonly BatchContext _context;

        public BatchRepository(BatchContext context)
        {
            _context = context;
        }

        public async Task<Batch?> GetByIdAsync(String Name)
        {
            return await _context.Batches.FindAsync(Name);
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
            // First, get the existing entity from the database
            var existingBatch = await _context.Batches
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Name == batch.Name);

            if (existingBatch != null)
            {
                // If the entity exists, update its properties
                _context.Entry(existingBatch).CurrentValues.SetValues(batch);
                _context.Entry(existingBatch).State = EntityState.Modified;
            }
            else
            {
                // If it's a new entity, add it
                await _context.Batches.AddAsync(batch);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is MySqlException mySqlEx && 
                                             mySqlEx.Number == 1062) // Duplicate entry
            {
                throw new Exception("A batch with this name already exists.", ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("The batch was modified by another process. Please refresh and try again.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving the batch.", ex);
            }
        }

        public async Task DeleteAsync(String Name)
        {
            var entity = await _context.Batches.FindAsync(Name);
            if (entity != null)
            {
                _context.Batches.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

