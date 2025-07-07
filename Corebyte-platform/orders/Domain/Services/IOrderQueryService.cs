using Corebyte_platform.orders.Domain.Model.Aggregates;
using Corebyte_platform.orders.Domain.Model.Queries;

namespace Corebyte_platform.orders.Domain.Services
{
    public interface IOrderQueryService
    {
        /// <summary>
        /// Handle the GetAllOrdersQuery.
        /// </summary>
        /// <returns>An IEnumerable containing all Order objects</returns>
        Task<IEnumerable<Order>> Handle(GetAllOrdersQuery query);
        Task<IEnumerable<Order>> Handle(GetOrderByIdQuery query);
        /// <summary>
        ///     Handle the GetOrderByCustomerQuery.
        /// </summary>
        /// <remarks>
        ///     This method handles the GetOrderByCustomerQuery. It returns all the orders for the given
        ///     customer.
        /// </remarks>
        /// <param name="query">The GetOrderByCustomerQuery query</param>
        /// <returns>An IEnumerable containing the Order objects</returns>
        Task<IEnumerable<Order>> Handle(GetOrderByCustomerQuery query);
        /// <summary>
        ///     Handle the GetOrderByProductQuery.
        /// </summary>
        /// <remarks>
        ///     This method handles the GetOrderByProductQuery. It returns the order for the given
        ///     product.
        /// </remarks>
        /// <param name="query">The GetOrderByProductQuery query</param>
        /// <returns>The Order object if found, or null otherwise</returns>
        Task<Order?> Handle(GetOrderByProductQuery query);
        /// <summary>
        ///     Handle the GetOrderByAmountAndTotalQuery.
        /// </summary>
        /// <remarks>
        ///     This method handles the GetOrderByAmountAndTotalQuery. It returns the order for the given amount and total.
        /// </remarks>
        /// <param name="query">The GetOrderByAmountAndTotalQuery query</param>
        /// <returns>The Order object if found, or null otherwise</returns>
        Task<Order?> Handle(GetOrderByAmountAndTotalQuery query);

    }
}
