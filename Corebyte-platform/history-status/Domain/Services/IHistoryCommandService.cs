using Corebyte_platform.history_status.Domain.Model.Aggregates;
using Corebyte_platform.history_status.Domain.Model.Commands;

namespace Corebyte_platform.history_status.Domain.Services
{
    public interface IHistoryCommandService
    {
        /// <summary>
        ///     Handle the create history command.
        /// </summary>
        /// <remarks>
        ///     This method handles the create history command. It checks if the history already exists for the
        ///     given customer, date, product, amount, total, and status.
        ///     If it does not exist, it creates a new history and adds it to the database.
        /// </remarks>
        /// <param name="command">CreateHistoryCommand command</param>
        /// <returns>The created history</returns>
        /// <exception cref="DuplicateHistoryException">Thrown when a history with the same details already exists</exception>
        Task<History> Handle(CreateHistoryCommand command);

        /// <summary>
        ///     Handle the delete histories by id command.
        /// </summary>
        /// <remarks>
        ///     This method deletes all history records for a specific id.
        /// </remarks>
        /// <param name="command">The delete command containing the id identifier</param>
        /// <returns>The number of records deleted</returns>
        Task<int> Handle(DeleteHistoriesByIdCommand command);

        /// <summary>
        ///     Handle the update history command.
        /// </summary>
        /// <remarks>
        ///     This method updates an existing history record with the provided data.
        /// </remarks>
        /// <param name="command">The update command containing the history data</param>
        /// <returns>The updated history record</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the history record is not found</exception>
        Task<History> Handle(UpdateHistoryCommand command);
    }
}
