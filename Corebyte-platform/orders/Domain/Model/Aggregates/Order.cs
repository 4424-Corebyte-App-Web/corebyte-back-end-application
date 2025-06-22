using Corebyte_platform.orders.Domain.Model.Commands;

namespace Corebyte_platform.orders.Domain.Model.Aggregates
{
    /// <summary>
    /// The Order aggregate.
    /// </summary>
    /// 
    public class Order
    {
        /// <summary>
        /// The Id of the order.
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        /// The customer of the order.
        /// </summary>
        public string Customer { get; private set; }
        
        /// <summary>
        /// The date of the order.
        /// </summary>
        public DateTime Date { get; private set; }
        
        /// <summary>
        /// The product of the order.
        /// </summary>
        public string Product { get; private set; }
        
        /// <summary>
        /// The amount of the order.
        /// </summary>
        public int Amount { get; private set; }
        
        /// <summary>
        /// The total of the order.
        /// </summary>
        public double Total { get; private set; }
        /// <summary>
        /// Initializes a new instance of the Order class.
        /// </summary>
        protected Order() 
        {
            Customer = string.Empty;
            Date = DateTime.Now;
            Product = string.Empty;
            Amount = 0;
            Total = 0.0;
        }
        /// <summary>
        /// Constructor for the Order aggregate.
        /// </summary>
        /// <remarks>
        /// The constructor is the command handler for the CreateOrderCommand.
        /// </remarks>
        public Order(CreateOrderCommand command)
        {
            Customer = command.customer;
            Date = command.date;
            Product = command.product;
            Amount = command.amount;
            Total = command.total;
        }

       

    }
}
