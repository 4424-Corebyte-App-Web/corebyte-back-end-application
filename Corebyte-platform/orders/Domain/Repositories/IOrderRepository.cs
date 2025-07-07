using Corebyte_platform.orders.Domain.Model.Aggregates;
using Corebyte_platform.orders.Domain.Model.Commands;
using Corebyte_platform.Shared.Domain.Repositories;


namespace Corebyte_platform.orders.Domain.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        /// <summary>
        /// Finds all orders for a specific customer
        /// </summary>
        /// <param name="customer">Customer name to search for</param>
        /// <returns>Collection of orders</returns>
        Task<IEnumerable<Order>> FindByCustomerAsync(string customer);
        Task<Order?> FindByProductAsync(string product);
        Task<Order?> FindByAmountAndTotalAsync(int amount, double total);
        Task<Order?> FindByUrl(string url);


        /// <summary>
        /// Search for an existing order with the same data
        /// </summary>
        /// <param name="command">Command with the order data to search for</param>
        /// <returns>The existing order if found, null otherwise</returns>
        Task<Order?> FindByDetailsAsync(CreateOrderCommand command);


        /// <summary>
        /// Deletes all orders for a specific id
        /// </summary>
        /// <param name="id">Id name to delete orders for</param>
        /// <returns>Number of records deleted</returns>
        Task<int> DeleteByIdAsync(int id);


        /// <summary>
        /// Finds an order with the same customer, product and date but different ID
        /// </summary>
        /// <param name="id">ID to exclude from search</param>
        /// <param name="customer">Customer name to search for</param>
        /// <param name="product">Product name to search for</param>
        /// <param name="date">Date to search for</param>
        /// <returns>Matching order if found, null otherwise</returns>
        Task<Order?> FindByDetailsExceptIdAsync(int id, string customer, string product, DateTime date);



    }
}
