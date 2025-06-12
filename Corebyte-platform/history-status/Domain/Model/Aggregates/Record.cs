using System.ComponentModel.DataAnnotations.Schema;
using Corebyte_platform.history_status.Domain.Model.Commands;
using Corebyte_platform.history_status.Domain.Model.ValueObjects;

namespace Corebyte_platform.history_status.Domain.Model.Aggregates
{
    /// <summary>
    ///     The Record aggregate.
    /// </summary>
    public class Record
    {
        /// <summary>
        ///     The Id of the record.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        ///     The customer_id of the record.
        /// </summary>
        public int customer_id { get; private set; }
        [NotMapped] // This prevents Entity Framework from trying to map this property to a database column
        public int customerId { get => customer_id; private set => customer_id = value; }
        /// <summary>
        ///     The type of the record.
        /// </summary>
        public string type { get; private set; }
        /// <summary>
        ///     The year of the record.
        /// </summary>
        public DateTime year { get; private set; }
        /// <summary>
        ///     The product of the record.
        /// </summary>
        public string product { get; private set; }
        /// <summary>
        ///     The batch of the record.
        /// </summary>
        public int batch { get; private set; }
        /// <summary>
        ///     The stock of the record.
        /// </summary>
        public int stock { get; private set; }

        protected Record()
        {
            type = string.Empty;
            year = DateTime.Now;
            product = string.Empty;
            batch = 0;
            stock = 0;
        }

        /// <summary>
        /// Constructor for the Record aggregate.
        /// </summary>
        /// <remarks>
        ///     The constructor is the command handler for the CreateRecordCommand.
        /// </remarks>
        /// <param name="command">The CreateRecordCommand command</param>
        public Record(CreateRecordCommand command)
        {
            customer_id = command.customerId.id;
            type = command.type;
            year = command.year;
            product = command.product;
            batch = command.batch;
            stock = command.stock;
        }

        /// <summary>
        /// Updates the record with new values
        /// </summary>
        /// <param name="customerId">New customerId</param>
        /// <param name="type">New type</param>
        /// <param name="year">New year</param>
        /// <param name="product">New product</param>
        /// <param name="batch">New batch</param>
        /// <param name="stock">New stock</param>
        public void Update(
            CustomerId customerId,
            string type,
            DateTime year,
            string product,
            int batch,
            int stock)
        {
            this.customer_id = customerId?.id ?? throw new ArgumentNullException(nameof(customerId));
            this.type = type ?? throw new ArgumentNullException(nameof(type));
            this.year = year;
            this.product = product ?? throw new ArgumentNullException(nameof(product));
            this.batch = batch;
            this.stock = stock;
        }
    }
}
