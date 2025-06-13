using Corebyte_platform.history_status.Domain.Model.Aggregates;
using Corebyte_platform.history_status.Domain.Model.Commands;
using Corebyte_platform.history_status.Domain.Repositories;
using Corebyte_platform.history_status.Domain.Services;
using Corebyte_platform.Shared.Domain.Repositories;

namespace Corebyte_platform.history_status.Application.Infernal.CommandServices
{
    ///<summary>
    /// Command service for Record aggregate
    ///</summary>
    /// <param name="recordRepository">The record repository</param>
    /// <param name="unitOfWork">The unit of work</param>
    public class RecordCommandService(IRecordRepository recordRepository, IUnitOfWork unitOfWork): IRecordCommandService
    {
        /// <summary>
        ///     Deletes a record by its ID.
        /// </summary>
        /// <param name="command">The DeleteRecordByIdCommand command</param>
        /// <returns>The number of records deleted</returns>
        public async Task<int> Handle(DeleteRecordByIdCommand command)
        {
            var deletedCount = await recordRepository.DeleteRecordByIdAsync(command.Id);
            await unitOfWork.CompleteAsync();
            
            return deletedCount;
        }
        /// <summary>
        ///     Updates a record by its ID.
        /// </summary>
        /// <param name="command">The UpdateRecordCommand command</param>
        /// <returns>The updated record</returns>
        public async Task<Record> Handle(UpdateRecordCommand command)
        {
            var existingRecord = await recordRepository.FindByIdAsync(command.id);
            if (existingRecord == null)
            {
                throw new KeyNotFoundException($"No record found with ID: {command.id}");
            }
            var duplicate = await recordRepository.FindRecordByDetailsExceptIdAsync(
                command.id, 
                command.CustomerId, 
                command.Type, 
                command.Product);
            if (duplicate != null) { 
                throw new InvalidOperationException("Another record with the same customer, type and product already exists");
            }
            existingRecord.Update(
                command.CustomerId,
                command.Type,
                command.Year,
                command.Product,
                command.Batch,
                command.Stock);
            await recordRepository.UpdateAsync(existingRecord);
            await unitOfWork.CompleteAsync();
            
            return existingRecord;
        }
        /// <summary>
        ///     Creates a new record.
        /// </summary>
        /// <param name="command">The CreateRecordCommand command</param>
        /// <returns>The created record</returns>
        public async Task<Record> Handle(CreateRecordCommand command) {
            var existingRecord = await recordRepository.FindRecordByDetailsAsync(command);
            if (existingRecord != null)
            {
                throw new InvalidOperationException("There is already another record with the same details");
            }
            var newRecord = new Record(command);

            try
            {
                await recordRepository.AddAsync(newRecord);
                await unitOfWork.CompleteAsync();
                return newRecord;
            }
            catch (Exception ex){ 
                throw new InvalidOperationException("An error occurred while creating the record", ex);
            }

        }
    }
}
