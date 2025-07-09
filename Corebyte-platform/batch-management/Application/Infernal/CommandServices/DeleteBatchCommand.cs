using System;
using MediatR;

namespace Corebyte_platform.batch_management.Application.Infernal.CommandServices
{
    public record DeleteBatchCommand(string Name) : IRequest<Unit>;
}


