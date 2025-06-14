using System;
using System.Threading.Tasks;
using Corebyte_platform.batch_management.Domain.Model.Aggregates;
using Corebyte_platform.batch_management.Domain.Model.Commands;
using Corebyte_platform.batch_management.Domain.Repositories;
using Corebyte_platform.batch_management.Domain.Common.Guards;

namespace Corebyte_platform.batch_management.Application.Infernal.CommandServices
{
    /// <summary>
    /// Interface for batch command operations
    /// </summary>
    public interface IBatchCommandService
    {
        /// <summary>
        /// Creates a new batch
        /// </summary>
        /// <param name="command">The create batch command</param>
        /// <returns>The ID of the created batch</returns>
        Task<string> CreateBatchAsync(CreateBatchCommand command);
        
        /// <summary>
        /// Updates an existing batch
        /// </summary>
        /// <param name="command">The update batch command</param>
        Task UpdateBatchAsync(UpdateBatchCommand command);
        
        /// <summary>
        /// Deletes a batch
        /// </summary>
        /// <param name="command">The delete batch command</param>
        Task DeleteBatchAsync(DeleteBatchCommand command);
    }
    
    /// <summary>
    /// Implementation of batch command operations
    /// </summary>
    public class BatchCommandService : IBatchCommandService
    {
        private readonly IBatchRepository _repository;
        
        /// <summary>
        /// Constructor for BatchCommandService
        /// </summary>
        /// <param name="repository">The batch repository</param>
        public BatchCommandService(IBatchRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        
        /// <inheritdoc/>
        public async Task<string> CreateBatchAsync(CreateBatchCommand command)
        {
            Guard.ThrowIfNull(command, nameof(command));
                
            var batch = new Batch
            {
                Id = Guid.NewGuid().ToString(),
                Name = command.Name,
                Type = command.Type,
                Status = "New", // Default status for new batches
                Temperature = "N/A", // Default temperature
                Amount = command.Amount,
                Total = command.Total,
                Date = DateTime.UtcNow,
                NLote = command.NLote
            };
            
            await _repository.AddAsync(batch);
            return batch.Id;
        }
        
        /// <inheritdoc/>
        public async Task UpdateBatchAsync(UpdateBatchCommand command)
        {
            Guard.ThrowIfNull(command, nameof(command));
                
            var batch = await _repository.GetByIdAsync(command.Id);
            if (batch == null)
                throw new InvalidOperationException($"Batch with ID {command.Id} not found");
                
            // Only update specific fields
            batch.Status = command.Status;
            batch.Temperature = command.Temperature;
            
            await _repository.UpdateAsync(batch);
        }
        
        /// <inheritdoc/>
        public async Task DeleteBatchAsync(DeleteBatchCommand command)
        {
            Guard.ThrowIfNull(command, nameof(command));
                
            // Verify the batch exists
            var batch = await _repository.GetByIdAsync(command.Id);
            if (batch == null)
                throw new InvalidOperationException($"Batch with ID {command.Id} not found");
                
            await _repository.DeleteAsync(command.Id);
        }
    }
}

