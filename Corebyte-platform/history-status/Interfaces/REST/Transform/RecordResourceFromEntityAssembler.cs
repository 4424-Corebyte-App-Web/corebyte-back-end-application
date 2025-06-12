using Corebyte_platform.history_status.Domain.Model.Aggregates;
using Corebyte_platform.history_status.Interfaces.REST.Resources;

namespace Corebyte_platform.history_status.Interfaces.REST.Transform
{
    public static class RecordResourceFromEntityAssembler
    {
        /// <summary>
        /// Assembles a RecordResource from a Record. 
        /// </summary>
        /// <param name="entity">The Record entity</param>
        /// <returns>
        /// A RecordResource assembled from the Record
        /// </returns>
        public static RecordResource ToResourceFromEntity(Record entity)=>
            new RecordResource(
                entity.Id, 
                entity.customer_id, 
                entity.type, 
                entity.year, 
                entity.product, 
                entity.batch, 
                entity.stock);

        /// <summary>
        /// Converts a collection of Record entities to a collection of RecordResource objects
        /// </summary>
        /// <param name="records">The collection of Record entities</param>
        /// <returns>A collection of RecordResource objects</returns>
        internal static IEnumerable<RecordResource>ToResourceFromEntity(IEnumerable<Record>records)=>
                        records.Select(record => new RecordResource(
                record.Id, 
                record.customer_id, 
                record.type, 
                record.year, 
                record.product, 
                record.batch, 
                record.stock));
    }
}
