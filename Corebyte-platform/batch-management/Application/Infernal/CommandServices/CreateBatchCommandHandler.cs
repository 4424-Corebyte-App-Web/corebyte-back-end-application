using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Corebyte_platform.batch_management.Domain.Model.Aggregates;
using Corebyte_platform.batch_management.Domain.Repositories;

namespace Corebyte_platform.batch_management.Application.Infernal.CommandServices
{
    public class CreateBatchCommandHandler : IRequestHandler<CreateBatchCommand, Guid>
    {
        private readonly IBatchRepository _repository;

        public CreateBatchCommandHandler(IBatchRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateBatchCommand request, CancellationToken cancellationToken)
        {
            var batch = new Batch(
                request.Name,
                request.Type,
                request.Status,
                request.Temperature,
                request.Amount,
                request.Total,
                request.Date,
                request.NLote);
            await _repository.AddAsync(batch);
            return batch.Id;
        }
    }
}

