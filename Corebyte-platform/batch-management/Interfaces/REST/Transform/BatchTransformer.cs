using Corebyte_platform.batch_management.Domain.Model.Aggregates;
using Corebyte_platform.batch_management.Interfaces.REST.Resources;
using Corebyte_platform.batch_management.Application.Infernal.CommandServices;

namespace Corebyte_platform.batch_management.Interfaces.REST.Transform
{
    public static class BatchTransformer
    {
        public static BatchResource ToResource(Batch batch) => new()
        {
            Id = batch.Id,
            Name = batch.Name,
            Type = batch.Type,
            Status = batch.Status,
            Temperature = batch.Temperature,
            Amount = batch.Amount,
            Total = batch.Total,
            Date = batch.Date,
            NLote = batch.NLote
        };

        public static Batch ToDomain(CreateBatchCommand cmd) => new(
            cmd.Name, cmd.Type, cmd.Status, cmd.Temperature,
            cmd.Amount, cmd.Total, cmd.Date, cmd.NLote);
    }
}
