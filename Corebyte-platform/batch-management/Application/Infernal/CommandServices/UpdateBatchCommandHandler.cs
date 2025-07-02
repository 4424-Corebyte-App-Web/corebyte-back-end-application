using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Corebyte_platform.batch_management.Domain.Repositories;

namespace Corebyte_platform.batch_management.Application.Infernal.CommandServices
{
    public class UpdateBatchCommandHandler : IRequestHandler<UpdateBatchCommand>
    {
        private readonly IBatchRepository _repository;

        public UpdateBatchCommandHandler(IBatchRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateBatchCommand request, CancellationToken cancellationToken)
        {
            var batch = await _repository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Batch {request.Id} not found");
            // map updated fields (assumes Batch has appropriate methods or setters)
            // batch.UpdateName(request.Name);
            await _repository.UpdateAsync(batch);
            return Unit.Value;
        }
    }
}

