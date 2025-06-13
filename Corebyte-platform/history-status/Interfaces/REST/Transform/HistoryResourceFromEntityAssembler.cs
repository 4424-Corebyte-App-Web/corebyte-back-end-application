using Corebyte_platform.history_status.Domain.Model.Aggregates;
using Corebyte_platform.history_status.Interfaces.REST.Resources;

namespace Corebyte_platform.history_status.Interfaces.REST.Transform
{
    public static class HistoryResourceFromEntityAssembler
    {
        /// <summary>
        /// Assembles a HistoryResource from a History. 
        /// </summary>
        /// <param name="entity">The History entity</param>
        /// <returns>
        /// A HistoryResource assembled from the History
        /// </returns>
        public static HistoryResource ToResourceFromEntity(History entity) => 
            new HistoryResource(entity.Id, entity.customer, entity.date, entity.product, entity.amount, entity.total, entity.status);

        /// <summary>
        /// Converts a collection of History entities to a collection of HistoryResource objects
        /// </summary>
        /// <param name="histories">The collection of History entities</param>
        /// <returns>A collection of HistoryResource objects</returns>
        internal static IEnumerable<HistoryResource> ToResourceFromEntity(IEnumerable<History> histories) =>
            histories.Select(history => new HistoryResource(
                history.Id, 
                history.customer, 
                history.date, 
                history.product, 
                history.amount, 
                history.total, 
                history.status));
    }
}