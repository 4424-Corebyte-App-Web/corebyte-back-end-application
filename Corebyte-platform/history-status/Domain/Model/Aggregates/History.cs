using Corebyte_platform.history_status.Domain.Model.Commands;
using Corebyte_platform.history_status.Domain.Model.ValueObjects;

namespace Corebyte_platform.history_status.Domain.Model.Aggregates
{
    /// <summary>
    ///     The History aggregate.
    /// </summary>
    public class History
    {
        
        /// <summary>
        ///     The Id of the history.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        ///     The customer of the history.
        /// </summary>
        public string customer { get; private set; }
        /// <summary>
        ///     The date of the history.
        /// </summary>
        public DateTime date { get; private set; }
        /// <summary>
        ///     The product of the history.
        /// </summary>
        public string product { get; private set; }
        /// <summary>
        ///     The amount of the history.
        /// </summary>
        public int amount { get; private set; }
        /// <summary>
        ///     The total of the history.
        /// </summary>
        public double total { get; private set; }
        /// <summary>
        ///     The status of the history.
        /// </summary>
        public Status status { get; set; }

        /// <summary>
        ///     Initializes a new instance of the History class.
        /// </summary>
        // Private constructor for Entity Framework
        private History() { }

        protected History(Status statusinitial) { 
            customer=string.Empty; 
            date=DateTime.Now;
            product=string.Empty;
            amount=0;
            total=0.0;
            status=statusinitial;
        }

        public void ChangeStatus(Status newStatus)
        {
            status = newStatus;
        }
        /// <summary>
        ///     Constructor for the History aggregate.
        /// </summary>
        /// <remarks>
        ///     The constructor is the command handler for the CreateHistoryCommand.
        /// </remarks>
        /// <param name="command">The CreateHistoryCommand command</param>
        
        public History(CreateHistoryCommand command)
        {
            customer = command.customer;
            date = command.date;
            product = command.product;
            amount = command.amount;
            total = command.total;
            status = Status.PENDING;
        }

        /// <summary>
        /// Updates the history record with new values
        /// </summary>
        /// <param name="customer">New customer name</param>
        /// <param name="date">New transaction date</param>
        /// <param name="product">New product name</param>
        /// <param name="amount">New product quantity</param>
        /// <param name="total">New total amount</param>
        /// <param name="status">New transaction status</param>
        public void Update(
            string customer,
            DateTime date,
            string product,
            int amount,
            double total,
            Status status)
        {
            this.customer = customer ?? throw new ArgumentNullException(nameof(customer));
            this.date = date;
            this.product = product ?? throw new ArgumentNullException(nameof(product));
            this.amount = amount >= 0 ? amount : throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative");
            this.total = total >= 0 ? total : throw new ArgumentOutOfRangeException(nameof(total), "Total cannot be negative");
            this.status = status;
        }
    }
}
