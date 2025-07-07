using Corebyte_platform.orders.Domain.Model.Aggregates;
using Corebyte_platform.orders.Domain.Model.Commands;


namespace Corebyte_platform.orders.Domain.Services
{
    public interface IOrderCommandService
    {
        /// <summary>
        ///     Handle the create order command.
        /// </summary>
        /// <remarks>
        ///     This method handles the create order command. It checks if the order already exists for the
        ///     given customer, date, product, amount, total, and status.
        ///     If it does not exist, it creates a new order and adds it to the database.
        /// </remarks>
        /// <param name="command">CreateOrderCommand command</param>
        /// <returns>The created order</returns>
        /// <exception cref="DuplicateOrderException">Thrown when an order with the same details already exists</exception>
        Task<Order> Handle(CreateOrderCommand command);
        /// <summary>
        ///     Handle the delete orders by id command.
        /// </summary>
        /// <remarks>
        ///     This method deletes all order records for a specific id.
        /// </remarks>
        /// <param name="command">The delete command containing the id identifier</param>
        /// <returns>The number of records deleted</returns>
        Task<Order> Handle(DeleteOrdersByIdCommand command);

    }
}
