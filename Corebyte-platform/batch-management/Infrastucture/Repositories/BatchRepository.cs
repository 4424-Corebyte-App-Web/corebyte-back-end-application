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
            // Check if the entity is already being tracked
            var existingEntity = _context.ChangeTracker.Entries<Batch>()
                .FirstOrDefault(e => e.Entity.Name == batch.Name)?.Entity;

            if (existingEntity != null)
            {
                // If the entity is already being tracked, detach it first
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            // Attach the entity and mark it as modified
            _context.Batches.Update(batch);

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

