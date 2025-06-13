using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Corebyte_platform.batch_management.Domain.Common.Repositories;

namespace Corebyte_platform.batch_management.Infrastructure.Data
{
    /// <summary>
    /// Base repository implementation using Entity Framework Core
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        
        /// <summary>
        /// Constructor for RepositoryBase
        /// </summary>
        /// <param name="context">The database context</param>
        protected RepositoryBase(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }
        
        /// <inheritdoc/>
        public virtual async Task<TEntity> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Entity ID cannot be null or empty", nameof(id));
                
            return await _dbSet.FindAsync(id);
        }
        
        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        
        /// <inheritdoc/>
        public virtual async Task AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
                
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        
        /// <inheritdoc/>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
                
            // Entity Framework will track changes to the entity
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        
        /// <inheritdoc/>
        public virtual async Task DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Entity ID cannot be null or empty", nameof(id));
                
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

