using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Corebyte_platform.batch_management.Domain.Repositories;

namespace Corebyte_platform.batch_management.Application.Infernal.CommandServices
{
    public class DeleteBatchCommandHandler : IRequestHandler<DeleteBatchCommand, Unit>
    {
        private readonly IBatchRepository _repository;

        public DeleteBatchCommandHandler(IBatchRepository repository)
        {
            _repository = repository;
        }

        async Task<Unit> IRequestHandler<DeleteBatchCommand, Unit>.Handle(DeleteBatchCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Name);
            return Unit.Value;
        }
    }
}

