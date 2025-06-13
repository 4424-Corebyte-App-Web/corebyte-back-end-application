using System;
using System.ComponentModel.DataAnnotations;

namespace Corebyte_platform.batch_management.Domain.Common.Models
{
    /// <summary>
    /// Base class for batch-related models to reduce code duplication
    /// </summary>
    public abstract class BatchBase
    {
        /// <summary>
        /// Unique identifier for the batch
        /// </summary>
        public string Id { get; set; }

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
        /// Current status of the batch
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        /// <summary>
        /// Current temperature of the batch
        /// </summary>
        [StringLength(20)]
        public string Temperature { get; set; }

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
        /// Date when the batch was created or last updated
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Lot number identifier
        /// </summary>
        [Required]
        [StringLength(20)]
        public string NLote { get; set; }
    }
}

