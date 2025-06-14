using System.Collections.Generic;
using System.Linq;
using Corebyte_platform.batch_management.Domain.Model.Aggregates;
using Corebyte_platform.batch_management.Domain.Model.Commands;
using Corebyte_platform.batch_management.Domain.Model.Queries;
using Corebyte_platform.batch_management.Interfaces.REST.Resources;

namespace Corebyte_platform.batch_management.Interfaces.REST.Transform
{
    /// <summary>
    /// Transformer for batch-related objects between domain and resource models
    /// </summary>
    public static class BatchTransform
    {
        /// <summary>
        /// Transform a domain Batch to a BatchResource
        /// </summary>
        /// <param name="batch">The domain batch entity</param>
        /// <returns>The batch resource</returns>
        public static BatchResource ToResource(this Batch batch)
        {
            if (batch == null)
                return null;
                
            return new BatchResource
            {
                Id = batch.Id,
                Name = batch.Name,
                Type = batch.Type,
                Status = batch.Status,
                Temperature = batch.Temperature,
                Amount = batch.Amount,
                Total = batch.Total,
                Date = batch.Date,
                NLote = batch.NLote
            };
        }
        
        /// <summary>
        /// Transform a collection of domain Batch entities to BatchResource objects
        /// </summary>
        /// <param name="batches">The collection of domain batch entities</param>
        /// <returns>The collection of batch resources</returns>
        public static IEnumerable<BatchResource> ToResourceList(this IEnumerable<Batch> batches)
        {
            return batches?.Select(batch => batch.ToResource());
        }
        
        /// <summary>
        /// Transform a CreateBatchResource to a CreateBatchCommand
        /// </summary>
        /// <param name="resource">The create batch resource</param>
        /// <returns>The create batch command</returns>
        public static CreateBatchCommand ToCommand(this CreateBatchResource resource)
        {
            if (resource == null)
                return null;
                
            return new CreateBatchCommand(
                resource.Name,
                resource.Type,
                resource.Amount,
                resource.Total,
                resource.NLote
            );
        }
        
        /// <summary>
        /// Transform an UpdateBatchResource to an UpdateBatchCommand
        /// </summary>
        /// <param name="resource">The update batch resource</param>
        /// <param name="id">The batch ID</param>
        /// <returns>The update batch command</returns>
        public static UpdateBatchCommand ToCommand(this UpdateBatchResource resource, string id)
        {
            if (resource == null)
                return null;
                
            return new UpdateBatchCommand(
                id,
                resource.Status,
                resource.Temperature
            );
        }
        
        /// <summary>
        /// Transform a BatchFilterResource to a GetBatchesQuery
        /// </summary>
        /// <param name="resource">The batch filter resource</param>
        /// <returns>The get batches query</returns>
        public static GetBatchesQuery ToQuery(this BatchFilterResource resource)
        {
            if (resource == null)
                return new GetBatchesQuery(null, null, null, null);
                
            return new GetBatchesQuery(
                resource.FromDate,
                resource.ToDate,
                resource.Type,
                resource.Status
            );
        }
    }
}

