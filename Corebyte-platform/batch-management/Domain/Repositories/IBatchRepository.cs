using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Corebyte_platform.batch_management.Domain.Model.Aggregates;

namespace Corebyte_platform.batch_management.Domain.Repositories
{
    public interface IBatchRepository
    {
        Task<Batch?> GetByIdAsync(string name);
        Task<IEnumerable<Batch>> ListAsync();
        Task AddAsync(Batch batch);
        Task UpdateAsync(Batch batch);
        Task DeleteAsync(string name);
    }
}

