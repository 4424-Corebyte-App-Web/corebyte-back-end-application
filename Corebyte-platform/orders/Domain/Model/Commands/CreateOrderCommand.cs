using Corebyte_platform.orders.Domain.Model.ValueObjects;

namespace Corebyte_platform.orders.Domain.Model.Commands
{
    /// <summary>
    /// command to create a order entry.
    /// </summary>
    /// <param name="customer">The customer obtained from provider</param>
    /// <param name="date">The date of the order</param>
    /// <param name="product">The product of the order</param>
    /// <param name="amount">The amount of the order</param>
    /// <param name="total">The total of the order</param>
    /// <param name="url">The URL of the order</param>
    public record CreateOrderCommand(string customer, DateTime date, Products product, int amount, double total, string url);
}
