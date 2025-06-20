using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Corebyte_platform.batch_management.Domain.Repositories;

namespace Corebyte_platform.batch_management.Application.Infernal.CommandServices
{
    public class DeleteBatchCommandHandler : IRequestHandler<DeleteBatchCommand>
    {
        private readonly IBatchRepository _repository;

        public DeleteBatchCommandHandler(IBatchRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteBatchCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}

