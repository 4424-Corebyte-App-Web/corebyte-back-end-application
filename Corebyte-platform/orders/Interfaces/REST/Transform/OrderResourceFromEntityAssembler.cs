using Corebyte_platform.orders.Domain.Model.Aggregates;
using Corebyte_platform.orders.Interfaces.REST.Resources;

namespace Corebyte_platform.orders.Interfaces.REST.Transform
{

    public static class OrderResourceFromEntityAssembler
    {
        /// <summary>
        /// Assembles a OrderResource from an Order.
        /// </summary>
        /// <param name="entity">The Order entity</param>
        /// <returns>
        /// A OrderResource assembled from the Order
        /// </returns>
        /// 

        public static OrderResource ToResourceFromEntity(Order entity) =>
            new OrderResource(entity.Id, entity.Customer, entity.Date, entity.Product.ToString(), entity.Amount, entity.Total, entity.Url);

        /// <summary>
        /// Converts a collection of Order entities to a collection of OrderResource objects
        /// </summary>
        /// <param name="orders">The collection of Order entities</param>
        /// <returns>
        /// A collection of OrderResource objects
        /// </returns>
        /// 

        internal static IEnumerable<OrderResource> ToResourceFromEntity(IEnumerable<Order> orders) =>
            orders.Select(order => new OrderResource(
                order.Id,
                order.Customer,
                order.Date,
                order.Product.ToString(),
                order.Amount,
                order.Total,
                order.Url));
    }
}
