using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Corebyte_platform.batch_management.Domain.Repositories;
using Corebyte_platform.batch_management.Interfaces.REST.Resources;
using Corebyte_platform.batch_management.Interfaces.REST.Transform;

namespace Corebyte_platform.batch_management.Application.Infernal.QueryServices
{
    public class ListBatchesQueryHandler : IRequestHandler<ListBatchesQuery, IEnumerable<BatchResource>>
    {
        private readonly IBatchRepository _repository;

        public ListBatchesQueryHandler(IBatchRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BatchResource>> Handle(ListBatchesQuery request, CancellationToken cancellationToken)
        {
            var batches = await _repository.ListAsync();
            return batches.Select(BatchTransformer.ToResource);
        }
    }
}

