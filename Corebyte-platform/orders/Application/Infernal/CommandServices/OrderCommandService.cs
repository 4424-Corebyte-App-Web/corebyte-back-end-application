using Corebyte_platform.orders.Domain.Model.Aggregates;
using Corebyte_platform.orders.Domain.Model.Commands;
using Corebyte_platform.orders.Domain.Repositories;
using Corebyte_platform.orders.Domain.Services;
using Corebyte_platform.orders.Domain.Exceptions;
using Corebyte_platform.Shared.Domain.Repositories;

namespace Corebyte_platform.orders.Application.Infernal.CommandServices
{
    /// <summary>
    /// Command service for Order aggregate
    /// </summary>
    /// <param name="orderRepository">The order repository</param>
    /// <param name="unitOfWork">The unit of work</param>"
    /// 
    public class OrderCommandService(IOrderRepository orderRepository, IUnitOfWork unitOfWork) : IOrderCommandService
    {
        /// <summary>
        /// Deletes an order by its ID.
        /// </summary>
        /// <param name="command">The DeleteOrdersByIdCommand command</param>
        /// <returns>THe number of orders deleted</returns>
        /// 

        public async Task<Order> Handle(DeleteOrdersByIdCommand command)
        {
            var order = await orderRepository.FindByIdAsync(command.id);
            if (order == null)
            {
                return null;
            }
            await orderRepository.DeleteByIdAsync(command.id);
            await unitOfWork.CompleteAsync();
            return order;
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="command">The CreateOrderCommand command</param>    
        /// <returns>The created order </returns>


        public async Task<Order> Handle(CreateOrderCommand command)
        {
            var existingOrder = await orderRepository.FindByDetailsAsync(command);
            if (existingOrder != null)
            {
                var message = $"An order with the same details already exists for customer {command.customer}, product {command.product}, and date {command.date:dd/MM/yyyy}.";
                throw new DuplicateOrderException(message);
            }
            var order = new Order(command);

            try
            {
                await orderRepository.AddAsync(order);
                await unitOfWork.CompleteAsync();
                return order;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new InvalidOperationException("An error occurred while creating the order.", ex);
            }
        }
    }
}
