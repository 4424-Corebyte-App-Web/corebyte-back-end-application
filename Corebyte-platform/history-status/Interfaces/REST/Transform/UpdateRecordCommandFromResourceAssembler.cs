using Corebyte_platform.history_status.Domain.Model.Commands;
using Corebyte_platform.history_status.Domain.Model.ValueObjects;
using Corebyte_platform.history_status.Interfaces.REST.Resources;

namespace Corebyte_platform.history_status.Interfaces.REST.Transform
{
    public static class UpdateRecordCommandFromResourceAssembler
    {
        public static UpdateRecordCommand ToCommandFromResource(int id, UpdateRecordResource resource) {
            var customerId = new CustomerId(resource.CustomerId);
            return new UpdateRecordCommand(id, customerId, resource.Type, resource.Year, resource.Product, resource.Batch, resource.Stock);
        }
    }
}