using Corebyte_platform.orders.Domain.Model.Commands;
using Corebyte_platform.orders.Domain.Model.ValueObjects;
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
        public static CreateOrderCommand ToCommandFromResource(CreateOrderResource resource)
{
    if (!Enum.TryParse<Products>(resource.product, true, out var product))
    {
        throw new ArgumentException($"Invalid product value: {resource.product}. Valid values are: {string.Join(", ", Enum.GetNames(typeof(Products)))}");
    }
    
    return new CreateOrderCommand(
        resource.customer, 
        resource.date, 
        product,  // Now passing the enum value
        resource.amount, 
        resource.total,
        resource.url
    );
}
    }
}
