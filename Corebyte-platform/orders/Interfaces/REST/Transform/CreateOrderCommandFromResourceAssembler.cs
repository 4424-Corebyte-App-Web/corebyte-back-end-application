using Corebyte_platform.orders.Domain.Model.Commands;
using Corebyte_platform.orders.Interfaces.REST.Resources;

namespace Corebyte_platform.orders.Interfaces.REST.Transform
{
   
    public static class CreateOrderCommandFromResourceAssembler
    {

        /// <summary>
        /// Assembles a CreateOrderCommand from a CreateOrderResource.
        /// </summary>
        /// <param name="resource">The CreateOrderResource resource</param>
        /// <returns>
        /// A create order command assembled from the CreateOrderResource
        /// </returns>
        /// 
        public static CreateOrderCommand ToCommandFromResource(CreateOrderResource resource) =>
        new CreateOrderCommand(resource.customer, resource.date, resource.product, resource.amount, resource.total);
    }
}
