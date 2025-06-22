using Corebyte_platform.authentication.Domain.Model.Aggregates;

namespace Corebyte_platform.authentication.Domain.Repositories;

public interface IUserRepository
{
    Task<User> FindByIdAsync(int id);
    Task<User> FindByEmailAsync(string email);
    Task<User> FindByEmailExceptIdAsync(int id, string email);
    Task<IEnumerable<User>> GetAllAsync();
    Task<IEnumerable<User>> SearchByNameAsync(string name);
    Task<User> GetUserByUsername(string username);
    Task<User> GetUserByEmail(string email);

    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteByIdAsync(int id);
}