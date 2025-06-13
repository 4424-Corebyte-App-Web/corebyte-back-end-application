using System;
using System.ComponentModel.DataAnnotations;
using Corebyte_platform.batch_management.Domain.Common.Models;

namespace Corebyte_platform.batch_management.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource model for batch data transfer
    /// </summary>
    public class BatchResource : BatchBase
    {
        // All properties are inherited from BatchBase
        // Add any BatchResource-specific properties or methods here
    }

    /// <summary>
    /// Resource model for creating a new batch
    /// </summary>
    public class CreateBatchResource
    {
        /// <summary>
        /// Name of the batch
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        /// <summary>
        /// Type of product in the batch
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
        
        /// <summary>
        /// Amount classification (small, medium, large)
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Amount { get; set; }
        
        /// <summary>
        /// Total quantity of the batch
        /// </summary>
        [Required]
        public decimal Total { get; set; }
        
        /// <summary>
        /// Lot number identifier
        /// </summary>
        [Required]
        [StringLength(20)]
        public string NLote { get; set; }
    }

    /// <summary>
    /// Resource model for updating a batch
    /// </summary>
    public class UpdateBatchResource
    {
        /// <summary>
        /// Current status of the batch
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        
        /// <summary>
        /// Current temperature of the batch
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Temperature { get; set; }
    }

    /// <summary>
    /// Resource model for batch filtering
    /// </summary>
    public class BatchFilterResource
    {
        /// <summary>
        /// Start date for filtering
        /// </summary>
        public DateTime? FromDate { get; set; }
        
        /// <summary>
        /// End date for filtering
        /// </summary>
        public DateTime? ToDate { get; set; }
        
        /// <summary>
        /// Type for filtering
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// Status for filtering
        /// </summary>
        public string Status { get; set; }
    }
}

