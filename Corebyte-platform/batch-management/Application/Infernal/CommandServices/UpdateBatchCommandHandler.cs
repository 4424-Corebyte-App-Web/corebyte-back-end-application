using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Corebyte_platform.batch_management.Domain.Repositories;
using Corebyte_platform.batch_management.Domain.Model.Aggregates;

namespace Corebyte_platform.batch_management.Application.Infernal.CommandServices
{
    public class UpdateBatchCommandHandler : IRequestHandler<UpdateBatchCommand, Unit>
    {
        private readonly IBatchRepository _repository;

        public UpdateBatchCommandHandler(IBatchRepository repository)
        {
            _repository = repository;
        }

        async Task<Unit> IRequestHandler<UpdateBatchCommand, Unit>.Handle(UpdateBatchCommand request, CancellationToken cancellationToken)
        {
            var batch = await _repository.GetByIdAsync(request.Name)
                ?? throw new KeyNotFoundException($"Batch with name '{request.Name}' not found");
                
            // Update the batch properties
            // Note: In a real application, you might want to add validation and business logic here
            var updatedBatch = new Batch(
                request.NewName ?? request.Name, // Use new name if provided, otherwise keep the existing name
                request.Type,
                request.Status,
                request.Temperature,
                request.Amount,
                request.Total,
                request.Date,
                request.NLote
            );
            
            await _repository.UpdateAsync(updatedBatch);
            return Unit.Value;
        }
    }
}

