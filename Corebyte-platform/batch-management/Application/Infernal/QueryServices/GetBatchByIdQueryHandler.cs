using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Corebyte_platform.batch_management.Domain.Repositories;
using Corebyte_platform.batch_management.Interfaces.REST.Resources;
using Corebyte_platform.batch_management.Interfaces.REST.Transform;

namespace Corebyte_platform.batch_management.Application.Infernal.QueryServices
{
    public class GetBatchByIdQueryHandler : IRequestHandler<GetBatchByIdQuery, BatchResource>
    {
        private readonly IBatchRepository _repository;

        public GetBatchByIdQueryHandler(IBatchRepository repository)
        {
            _repository = repository;
        }

        public async Task<BatchResource> Handle(GetBatchByIdQuery request, CancellationToken cancellationToken)
        {
            var batch = await _repository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Batch {request.Id} not found");
            return BatchTransformer.ToResource(batch);
        }
    }
}

