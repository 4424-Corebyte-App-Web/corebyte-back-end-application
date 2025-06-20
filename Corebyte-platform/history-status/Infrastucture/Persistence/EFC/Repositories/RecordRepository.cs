using Corebyte_platform.history_status.Domain.Model.Aggregates;
using Corebyte_platform.history_status.Domain.Model.Commands;
using Corebyte_platform.history_status.Domain.Repositories;
using Corebyte_platform.history_status.Domain.Model.ValueObjects;
using Corebyte_platform.Shared.Infrastructure.Persistence.EFC.Repositories;
using Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Corebyte_platform.history_status.Infrastucture.Persistence.EFC.Repositories
{
    /// <summary>
    ///     The RecordRepository class.
    /// </summary>
    public class RecordRepository(AppDbContext context):BaseRepository<Record>(context),IRecordRepository
    {
        /// <summary>
        ///     Finds all the records for the given customer id.
        /// </summary>
        /// <param name="customerId">The customer id to search</param>
        /// <returns>An IEnumerable containing the record objects</returns>
        public async Task<IEnumerable<Record>>findByCustomerIdAsync(CustomerId customerId)
        {
            return await Context.Set<Record>().Where(r => r.customer_id == customerId.id).ToListAsync();
        }
        /// <summary>
        ///     Finds the record for the given stock.
        /// </summary>
        /// <param name="stock">The stock to search</param>
        /// <returns>The record object</returns>
        public async Task<Record?>findByStockAsync(int stock)
        {
            return await Context.Set<Record>().FirstOrDefaultAsync(r => r.stock == stock);
        }
        /// <summary>
        ///     Finds the record for the given type and product.
        /// </summary>
        /// <param name="type">The type to search</param>
        /// <param name="product">The product to search</param>
        /// <returns>The record object</returns>
        public async Task<Record?>findByTypeAndProductAsync(string type, string product)
        {
            return await Context.Set<Record>().FirstOrDefaultAsync(r => r.type == type && r.product == product);
        }
        /// <summary>
        ///     Finds the record for the given details.
        /// </summary>
        /// <param name="command">The command to search</param>
        /// <returns>The record object</returns>
        public async Task<Record?>FindRecordByDetailsAsync(CreateRecordCommand command)
        {
            return await Context.Set<Record>()
                .FirstOrDefaultAsync(r => 
                    r.customer_id == command.customerId.id &&
                    r.type == command.type &&
                    r.product == command.product &&
                    r.batch == command.batch &&
                    r.stock == command.stock);
        }
        /// <summary>
        ///     Deletes the record with the given id.
        /// </summary>
        /// <param name="id">The id to delete</param>
        /// <returns>The number of rows affected</returns>
        public async Task<int>DeleteRecordByIdAsync(int id)
        {
            var records = await Context.Set<Record>().Where(r => r.Id == id).ToListAsync();
            if (!records.Any()) return 0;
            Context.Set<Record>().RemoveRange(records);
            await Context.SaveChangesAsync();

            return records.Count;
        }

        /// <summary>
        ///     Finds the record for the given details except the given id.
        /// </summary>
        /// <param name="id">The id to search</param>
        /// <param name="customerId">The customer id to search</param>
        /// <param name="type">The type to search</param>
        /// <param name="product">The product to search</param>
        /// <returns>The record object</returns>
        public async Task<Record?> FindRecordByDetailsExceptIdAsync(int id, CustomerId customerId, string type, string product)
        {
            return await Context.Set<Record>()
                .FirstOrDefaultAsync(r => 
                    r.Id != id &&
                    r.customer_id == customerId.id &&
                    r.type == type &&
                    r.product == product);
        }

        public async Task UpdateAsync(Record record)
        {
            Context.Entry(record).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
        
    }
}
