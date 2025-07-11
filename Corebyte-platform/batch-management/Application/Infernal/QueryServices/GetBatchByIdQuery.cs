using System;
using MediatR;
using Corebyte_platform.batch_management.Interfaces.REST.Resources;

namespace Corebyte_platform.batch_management.Application.Infernal.QueryServices
{
    public record GetBatchByIdQuery(String Name) : IRequest<BatchResource>;
}


