using Corebyte_platform.orders.Domain.Model.Aggregates;
using Corebyte_platform.orders.Domain.Model.Commands;
using Corebyte_platform.orders.Domain.Repositories;
using Corebyte_platform.Shared.Domain.Repositories;
using Corebyte_platform.Shared.Infrastructure.Persistence.EFC.Repositories;
using Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;


namespace Corebyte_platform.orders.Infrastucture.Persistence.EFC.Repositories
{

    /// <summary>
    /// The OrderRepository class.
    /// </summary>
    /// 
    public class OrderRepository(AppDbContext context) : BaseRepository<Order>(context), IOrderRepository
    {
        /// <summary>
        /// Finds all orders for the given customer.
        /// </summary>
        /// <param name="customer">The customer to search.</param>}
        /// <returns>An IEnumerable containing the order objects.</returns>
        /// 

        public async Task<IEnumerable<Order>> FindByCustomerAsync(string customer)
        {
            return await Context.Set<Order>().Where(o => o.Customer == customer).ToListAsync();
        }

        /// <summary>
        /// Finds the order for the given product.
        /// </summary>
        /// <param name="product">The product to search.</param>
        /// <returns>The order object.</returns>
        /// 
        public async Task<Order?> FindByProductAsync(string product)
        {
            return await Context.Set<Order>().FirstOrDefaultAsync(o => o.Product == product);
        }

        /// <summary>
        /// Finds the order for the given amount and total.
        /// </summary>
        /// <param name="amount">The amount to search.</param>
        /// <param name="total">The total to search</param>
        /// <returns>The order object.</returns>
        /// 
        public async Task<Order?> FindByAmountAndTotalAsync(int amount, double total)
        {
            return await Context.Set<Order>()
                .FirstOrDefaultAsync(o => o.Amount == amount && o.Total == total);
        }

        /// <summary>
        /// Finds the order for the given details.
        /// </summary>
        /// <param name="command">The command to search.</param>
        /// <returns>The order object.</returns>
        /// 
        public async Task<Order?> FindByDetailsAsync(CreateOrderCommand command)
        {
            return await Context.Set<Order>()
                .FirstOrDefaultAsync(o => o.Customer == command.customer &&
                                          o.Product == command.product &&
                                          o.Date == command.date);
        }

        /// <summary>
        /// Finds the order for the given details except the given id.
        /// </summary>
        /// <pram name="id">The id to search.</param>
        /// <param name="customer">The customer to search.</param>
        /// <param name="product">The product to search.</param>
        /// <param name="date">The date to search.</param>  
        /// <returns>The order object</returns>
        /// 
        public async Task<Order?> FindByDetailsExceptIdAsync(int id, string customer, string product, DateTime date)
        {
            return await Context.Set<Order>()
                .FirstOrDefaultAsync(o => o.Id != id && o.Customer == customer &&
                                          o.Product == product && o.Date == date);
        }

        /// <summary>
        /// Deletes the order with the given id.
        /// </summary>
        /// <param name="id">The id to delete.</param>
        /// <returns>The number of rows affected</returns>
        public async Task<int> DeleteByIdAsync(int id)
        {
            var orders= await Context.Set<Order>().Where(o => o.Id == id).ToListAsync();
            if(!orders.Any())
            {
                return 0; // No orders found with the given id
            }
            Context.Set<Order>().RemoveRange(orders);
            await Context.SaveChangesAsync();
            return orders.Count; // Return the number of deleted orders
        }
       

    }
}
