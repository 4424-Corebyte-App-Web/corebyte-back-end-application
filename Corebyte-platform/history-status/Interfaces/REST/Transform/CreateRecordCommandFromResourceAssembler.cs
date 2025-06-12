using Corebyte_platform.history_status.Domain.Model.Commands;
using Corebyte_platform.history_status.Domain.Model.ValueObjects;
using Corebyte_platform.history_status.Interfaces.REST.Resources;

namespace Corebyte_platform.history_status.Interfaces.REST.Transform
{
    public static class CreateRecordCommandFromResourceAssembler
    {
        /// <summary>
        /// Assembles a CreateRecordCommand from a CreateRecordResource. 
        /// </summary>
        /// <param name="resource">The CreateRecordResource resource</param>
        /// <returns>
        /// A CreateRecordCommand assembled from the CreateRecordResource
        /// </returns>
        public static CreateRecordCommand ToCommandFromResource(CreateRecordResource resource) =>
            new CreateRecordCommand(
                new CustomerId(resource.customerId), 
                resource.type, 
                resource.year, 
                resource.product, 
                resource.batch, 
                resource.stock);
    }
}
