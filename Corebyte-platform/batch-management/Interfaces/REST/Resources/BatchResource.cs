using System;

namespace Corebyte_platform.batch_management.Interfaces.REST.Resources
{
    public class BatchResource
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Status { get; set; } = null!;
        public double Temperature { get; set; }
        public string Amount { get; set; } = null!;
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public string NLote { get; set; } = null!;
    }
}