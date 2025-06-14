using System;

namespace Corebyte_platform.batch_management.Domain.Model.Commands
{
    /// <summary>
    /// Command for creating a new batch
    /// </summary>
    public class CreateBatchCommand
    {
        /// <summary>
        /// Constructor for CreateBatchCommand
        /// </summary>
        public CreateBatchCommand(string name, string type, string amount, decimal total, string nlote)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
            Total = total;
            NLote = nlote ?? throw new ArgumentNullException(nameof(nlote));
        }
        
        public string Name { get; }
        public string Type { get; }
        public string Amount { get; }
        public decimal Total { get; }
        public string NLote { get; }
    }
    
    /// <summary>
    /// Command for deleting a batch
    /// </summary>
    public class DeleteBatchCommand
    {
        /// <summary>
        /// Constructor for DeleteBatchCommand
        /// </summary>
        public DeleteBatchCommand(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }
        
        public string Id { get; }
    }
    
    /// <summary>
    /// Command for updating a batch
    /// </summary>
    public class UpdateBatchCommand
    {
        /// <summary>
        /// Constructor for UpdateBatchCommand
        /// </summary>
        public UpdateBatchCommand(string id, string status, string temperature)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Status = status ?? throw new ArgumentNullException(nameof(status));
            Temperature = temperature ?? throw new ArgumentNullException(nameof(temperature));
        }
        
        public string Id { get; }
        public string Status { get; }
        public string Temperature { get; }
    }
}

