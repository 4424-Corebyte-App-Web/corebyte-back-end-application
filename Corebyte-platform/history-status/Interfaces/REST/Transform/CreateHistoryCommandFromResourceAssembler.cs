using Corebyte_platform.history_status.Domain.Model.Commands;
using Corebyte_platform.history_status.Interfaces.REST.Resources;

namespace Corebyte_platform.history_status.Interfaces.REST.Transform
{
    public static class CreateHistoryCommandFromResourceAssembler
    {
        /// <summary>
        /// Assembles a CreateHistoryCommand from a CreateHistoryResource. 
        /// </summary>
        /// <param name="resource">The CreateHistoryResource resource</param>
        /// <returns>
        /// A CreateHistoryCommand assembled from the CreateHistoryResource
        /// </returns>
        public static CreateHistoryCommand ToCommandFromResource(CreateHistoryResource resource)=>
            new CreateHistoryCommand(resource.customer, resource.date, resource.product, resource.amount, resource.total, resource.status);
    }
}