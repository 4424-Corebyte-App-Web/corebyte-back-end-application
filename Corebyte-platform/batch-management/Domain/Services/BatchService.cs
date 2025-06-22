using System;
using System.Threading.Tasks;
using Corebyte_platform.batch_management.Domain.Repositories;

namespace Corebyte_platform.batch_management.Domain.Services
{
    public class BatchService
    {
        private readonly IBatchRepository _repository;

        public BatchService(IBatchRepository repository)
        {
            _repository = repository;
        }

        public async Task ChangeStatusAsync(Guid id, string newStatus)
        {
            var batch = await _repository.GetByIdAsync(id)
                ?? throw new InvalidOperationException($"Batch with id {id} not found");
            // Assuming Batch has a method to update status
            // batch.UpdateStatus(newStatus);
            await _repository.UpdateAsync(batch);
        }
    }
}

