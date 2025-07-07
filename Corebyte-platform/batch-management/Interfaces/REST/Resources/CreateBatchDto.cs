using System;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Corebyte_platform.batch_management.Interfaces.REST.Resources
{
    public class CreateBatchDto
    {
        [Required]
        [SwaggerSchema("Nombre del batch")]
        public string Name { get; set; } = null!;

        [Required]
        [SwaggerSchema("Tipo de batch")]
        public string Type { get; set; } = null!;

        [Required]
        [SwaggerSchema("Estado actual del batch")]
        public string Status { get; set; } = null!;

        [Required]
        [SwaggerSchema("Temperatura del batch")]
        public double Temperature { get; set; }

        [Required]
        [SwaggerSchema("Cantidad producida")]
        public string Amount { get; set; } = null!;

        [Required]
        [SwaggerSchema("Total producido")]
        public decimal Total { get; set; }

        [Required]
        [SwaggerSchema("Fecha del batch")]
        public DateTime Date { get; set; }

        [Required]
        [SwaggerSchema("Número de lote")]
        public string NLote { get; set; } = null!;
    }
}