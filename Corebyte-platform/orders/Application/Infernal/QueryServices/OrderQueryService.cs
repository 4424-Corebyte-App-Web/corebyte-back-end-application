using Corebyte_platform.orders.Domain.Model.Aggregates;
using Corebyte_platform.orders.Domain.Model.Queries;
using Corebyte_platform.orders.Domain.Repositories;
using Corebyte_platform.orders.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Corebyte_platform.orders.Application.Infernal.QueryServices
{
    /// <summary>
    /// Query service for Order aggregate
    /// </summary>
    /// <param name="orderRepository">The order repository</param>"
    /// 
    public class OrderQueryService(IOrderRepository orderRepository) : IOrderQueryService
    {
        /// <summary>
        ///     Gets all orders.
        /// </summary>
        /// <param name="query">The GetAllOrdersQuery query</param>
        /// <returns>A list of orders</returns>
        public async Task<IEnumerable<Order>> Handle(GetAllOrdersQuery query)
        {
            return await orderRepository.ListAsync();
        }
        /// <summary>
        ///     Gets orders by customer.
        /// </summary>
        /// <param name="query">The GetOrderByCustomerQuery query</param>
        /// <returns>A list of orders</returns>
        public async Task<IEnumerable<Order>> Handle(GetOrderByCustomerQuery query)
        {
            return await orderRepository.FindByCustomerAsync(query.customer);
        }
        /// <summary>
        ///     Gets orders by product.
        /// </summary>
        /// <param name="query">The GetOrderByProductQuery query</param>
        /// <returns>An order object</returns>
        public async Task<Order?> Handle(GetOrderByProductQuery query)
        {
            return await orderRepository.FindByProductAsync(query.products);
        }
        /// <summary>
        ///     Gets orders by amount and total.
        /// </summary>
        /// <param name="query">The GetOrderByAmountAndTotalQuery query</param>
        /// <returns>An order object</returns>
        public async Task<Order?> Handle(GetOrderByAmountAndTotalQuery query)
        {
            return await orderRepository.FindByAmountAndTotalAsync(query.amount, query.total);
        }

        /// <summary>
        /// Retrieves an order based on the specified URL.
        /// </summary>
        /// <param name="query">The GetOrderByUrlQuery query</param>
        /// <returns>An order object</returns>

        public async Task<Order?> Handle(GetOrderByUrlQuery query)
        {
            return await orderRepository.FindByUrl(query.url);
        }


        /// <summary>
        /// 
        public async Task<IEnumerable<Order>> Handle(GetOrderByIdQuery query)
        {
            var order = await orderRepository.FindByIdAsync(query.Id);
            return order != null ? new List<Order> { order } : Enumerable.Empty<Order>();

        }
    }

}
