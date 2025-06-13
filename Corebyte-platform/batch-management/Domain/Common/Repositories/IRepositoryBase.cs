using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corebyte_platform.batch_management.Domain.Common.Repositories
{
    /// <summary>
    /// Base repository interface for entity operations
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get an entity by its ID
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>The entity if found, null otherwise</returns>
        Task<TEntity> GetByIdAsync(string id);
        
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>Collection of all entities</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();
        
        /// <summary>
        /// Add a new entity
        /// </summary>
        /// <param name="entity">The entity to add</param>
        Task AddAsync(TEntity entity);
        
        /// <summary>
        /// Update an existing entity
        /// </summary>
        /// <param name="entity">The entity to update</param>
        Task UpdateAsync(TEntity entity);
        
        /// <summary>
        /// Delete an entity by ID
        /// </summary>
        /// <param name="id">The ID of the entity to delete</param>
        Task DeleteAsync(string id);
    }
}

