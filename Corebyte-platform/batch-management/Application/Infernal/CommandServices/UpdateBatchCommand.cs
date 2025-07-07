using System;
using MediatR;

namespace Corebyte_platform.batch_management.Application.Infernal.CommandServices
{
    public record UpdateBatchCommand(
        string Name,
        string NewName,
        string Type,
        string Status,
        double Temperature,
        string Amount,
        decimal Total,
        DateTime Date,
        string NLote
    ) : IRequest;
}


