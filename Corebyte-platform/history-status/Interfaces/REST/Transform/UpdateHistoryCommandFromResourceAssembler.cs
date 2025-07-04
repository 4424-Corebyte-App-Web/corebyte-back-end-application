using Corebyte_platform.history_status.Domain.Model.Commands;
using Corebyte_platform.history_status.Interfaces.REST.Resources;

namespace Corebyte_platform.history_status.Interfaces.REST.Transform
{
    public static class UpdateHistoryCommandFromResourceAssembler
    {
        public static UpdateHistoryCommand ToCommandFromResource(int id, UpdateHistoryResource resource) {
            return new UpdateHistoryCommand(
                id,
                resource.Customer, 
                resource.Date,
                resource.Product,
                resource.Amount,
                resource.Total,
                resource.Status);
        }
    }
}