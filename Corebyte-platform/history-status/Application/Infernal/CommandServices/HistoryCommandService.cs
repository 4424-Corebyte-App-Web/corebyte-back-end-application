using Corebyte_platform.history_status.Domain.Model.Aggregates;
using Corebyte_platform.history_status.Domain.Model.Commands;
using Corebyte_platform.history_status.Domain.Repositories;
using Corebyte_platform.history_status.Domain.Services;
using Corebyte_platform.history_status.Domain.Exceptions;
using Corebyte_platform.Shared.Domain.Repositories;

namespace Corebyte_platform.history_status.Application.Infernal.CommandServices
{
    ///<summary>
    /// Command service for History aggregate
    ///</summary>
    /// <param name="historyRepository">The history repository</param>
    /// <param name="unitOfWork">The unit of work</param>
    public class HistoryCommandService(IHistoryRepository historyRepository, IUnitOfWork unitOfWork): IHistoryCommandService
    {
        /// <summary>
        ///     Deletes a history by its ID.
        /// </summary>
        /// <param name="command">The DeleteHistoriesByIdCommand command</param>
        /// <returns>The number of histories deleted</returns>
        public async Task<int> Handle(DeleteHistoriesByIdCommand command)
        {
            var deletedCount = await historyRepository.DeleteByIdAsync(command.id);

            await unitOfWork.CompleteAsync();
            
            return deletedCount;
        }
        /// <summary>
        ///     Updates a history by its ID.
        /// </summary>
        /// <param name="command">The UpdateHistoryCommand command</param>
        /// <returns>The updated history</returns>
        public async Task<History> Handle(UpdateHistoryCommand command)
        {
            var existingHistory = await historyRepository.FindByIdAsync(command.Id);
            if (existingHistory == null)
            {
                throw new KeyNotFoundException($"No history found with ID: {command.Id}");
            }

            var duplicate = await historyRepository.FindByDetailsExceptIdAsync(
                command.Id, 
                command.Customer, 
                command.Product, 
                command.Date);

            if (duplicate != null)
            {
                throw new InvalidOperationException(
                    "Another record already exists with the same customer, product, and date.");
            }

            existingHistory.Update(
                command.Customer,
                command.Date,
                command.Product,
                command.Amount,
                command.Total,
                command.Status);

            await historyRepository.UpdateAsync(existingHistory);
            await unitOfWork.CompleteAsync();
            
            return existingHistory;
        }
        /// <summary>
        ///     Creates a new history.
        /// </summary>
        /// <param name="command">The CreateHistoryCommand command</param>
        /// <returns>The created history</returns>
        public async Task<History> Handle(CreateHistoryCommand command) {
            var existingRecord = await historyRepository.FindByDetailsAsync(command);

            if (existingRecord != null)
            {
                var message = $"There is already a history for the client '{command.customer}' with the product'{command.product}'on the date {command.date:dd/MM/yyyy}";
                throw new DuplicateHistoryException(message);
            }

            var history = new History(command);

            try {
                await historyRepository.AddAsync(history);
                await unitOfWork.CompleteAsync();
                return history;
            } catch (Exception ex) {
                throw new InvalidOperationException("Failed to create history record. Please try again later.", ex);
            }
        }
    }
}
