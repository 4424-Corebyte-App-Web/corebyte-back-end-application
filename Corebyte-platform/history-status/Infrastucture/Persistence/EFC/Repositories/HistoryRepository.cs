using Corebyte_platform.history_status.Domain.Model.Aggregates;
using Corebyte_platform.history_status.Domain.Model.Commands;
using Corebyte_platform.history_status.Domain.Repositories;
using Corebyte_platform.Shared.Infrastructure.Persistence.EFC.Repositories;
using Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Corebyte_platform.history_status.Infrastucture.Repositories
{
    /// <summary>
    ///     The HistoryRepository class.
    /// </summary>
    public class HistoryRepository(AppDbContext context) : BaseRepository<History>(context), IHistoryRepository
    {
        /// <summary>
        ///     Finds all the history for the given customer.
        /// </summary>
        /// <param name="customer">The customer to search</param>
        /// <returns>An IEnumerable containing the history objects</returns>
        public async Task<IEnumerable<History>> findByCustomerAsync(string customer)
        {
            return await Context.Set<History>().Where(h => h.customer == customer).ToListAsync();
        }

        /// <summary>
        ///     Finds the history for the given product.
        /// </summary>
        /// <param name="product">The product to search</param>
        /// <returns>The history object</returns>
        public async Task<History?> findByProductAsync(string product)
        {
            return await Context.Set<History>().FirstOrDefaultAsync(h => h.product == product);
        }

        /// <summary>
        ///     Finds the history for the given status.
        /// </summary>
        /// <param name="status">The status to search</param>
        /// <returns>The history object</returns>
        public async Task<History?> findByStatusAsync(string status)
        {
            return await Context.Set<History>().FirstOrDefaultAsync(h => h.status == status);
        }

        /// <summary>
        ///     Finds the history for the given amount and total.
        /// </summary>
        /// <param name="amount">The amount to search</param>
        /// <param name="total">The total to search</param>
        /// <returns>The history object</returns>
        public async Task<History?> findByAmountAndTotalAsync(int amount, double total)
        {
            return await Context.Set<History>().FirstOrDefaultAsync(h => h.amount == amount && h.total == total);
        }

        /// <summary>
        ///     Finds the history for the given details.
        /// </summary>
        /// <param name="command">The command to search</param>
        /// <returns>The history object</returns>
        public async Task<History?> FindByDetailsAsync(CreateHistoryCommand command)
        {
            return await Context.Set<History>()
                .FirstOrDefaultAsync(h => 
                    h.customer == command.customer &&
                    h.product == command.product &&
                    h.date.Date == command.date.Date);
        }

        /// <summary>
        ///     Finds the history for the given details except the given id.
        /// </summary>
        /// <param name="id">The id to search</param>
        /// <param name="customer">The customer to search</param>
        /// <param name="product">The product to search</param>
        /// <param name="date">The date to search</param>
        /// <returns>The history object</returns>
        public async Task<History?> FindByDetailsExceptIdAsync(int id, string customer, string product, DateTime date)
        {
            return await Context.Set<History>()
                .FirstOrDefaultAsync(h => 
                    h.Id != id &&
                    h.customer == customer &&
                    h.product == product &&
                    h.date.Date == date.Date);
        }

        /// <summary>
        ///     Updates the given history.
        /// </summary>
        /// <param name="history">The history to update</param>
        public async Task UpdateAsync(History history)
        {
            if (history == null)
                throw new ArgumentNullException(nameof(history));

            Context.Set<History>().Update(history);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        ///     Deletes the history with the given id.
        /// </summary>
        /// <param name="id">The id to delete</param>
        /// <returns>The number of rows affected</returns>
        public async Task<int> DeleteByIdAsync(int id)
        {
            var histories = await Context.Set<History>()
                .Where(h => h.Id == id)
                .ToListAsync();

            if (!histories.Any())
                return 0;

            Context.Set<History>().RemoveRange(histories);
            await Context.SaveChangesAsync();
            
            return histories.Count;
        }
    }
}
