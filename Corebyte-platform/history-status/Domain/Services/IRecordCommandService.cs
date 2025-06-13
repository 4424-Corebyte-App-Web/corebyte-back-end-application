using Corebyte_platform.history_status.Domain.Model.Aggregates;
using Corebyte_platform.history_status.Domain.Model.Commands;

namespace Corebyte_platform.history_status.Domain.Services
{
    public interface IRecordCommandService
    {
        /// <summary>
        ///     Handle the create record command.
        /// </summary>
        /// <remarks>
        ///     This method handles the create record command. It checks if the record already exists for the
        ///     given customer, type, year, product, batch, and stock.
        ///     If it does not exist, it creates a new record and adds it to the database.
        /// </remarks>
        /// <param name="command">CreateRecordCommand command</param>
        /// <returns>The created record</returns>
        Task<Record> Handle(CreateRecordCommand command);
        /// <summary>
        ///     Handle the delete records by id command.
        /// </summary>
        /// <remarks>
        ///     This method deletes all record entries for a specific id.
        /// </remarks>
        /// <param name="command">The delete command containing the id identifier</param>
        /// <returns>The number of records deleted</returns>
        Task<int> Handle(DeleteRecordByIdCommand command);
        /// <summary>
        ///     Handle the update record command.
        /// </summary>
        /// <remarks>
        ///     This method updates an existing record with the provided data.
        /// </remarks>
        /// <param name="command">The update command containing the record data</param>
        /// <returns>The updated record</returns>
        Task<Record> Handle(UpdateRecordCommand command);
    }
}
