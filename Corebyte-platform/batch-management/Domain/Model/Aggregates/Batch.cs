using System;

namespace Corebyte_platform.batch_management.Domain.Model.Aggregates
{
    public class Batch
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Status { get; private set; }
        public double Temperature { get; private set; }
        public string Amount { get; private set; }
        public decimal Total { get; private set; }
        public DateTime Date { get; private set; }
        public string NLote { get; private set; }

        public Batch(string name, string type, string status, double temperature, string amount, decimal total, DateTime date, string nLote)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty", nameof(name));
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Type cannot be null or empty", nameof(type));
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status cannot be null or empty", nameof(status));
            if (temperature < 0)
                throw new ArgumentOutOfRangeException(nameof(temperature), "Temperature cannot be negative");
            if (total < 0)
                throw new ArgumentOutOfRangeException(nameof(total), "Total cannot be negative");
            if (date > DateTime.UtcNow)
                throw new ArgumentOutOfRangeException(nameof(date), "Date cannot be in the future");
            if (string.IsNullOrWhiteSpace(nLote))
                throw new ArgumentException("NLote cannot be null or empty", nameof(nLote));

            Id = Guid.NewGuid();
            Name = name;
            Type = type;
            Status = status;
            Temperature = temperature;
            Amount = amount;
            Total = total;
            Date = date;
            NLote = nLote;
        }

        protected Batch() { }
    }
}

