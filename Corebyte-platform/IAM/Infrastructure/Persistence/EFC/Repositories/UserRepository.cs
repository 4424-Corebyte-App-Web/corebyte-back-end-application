
using Corebyte_platform.IAM.Domain.Model.Aggregates;
using Corebyte_platform.IAM.Domain.Repositories;
using Corebyte_platform.Shared.Infrastructure.Persistence.EFC.Repositories;
using Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Corebyte_platform.IAM.Infrastructure.Persistence.EFC.Repositories;

/**
 * <summary>
 *     The user repository
 * </summary>
 * <remarks>
 *     This repository is used to manage users
 * </remarks>
 */
public class UserRepository(AppDbContext context) : BaseRepository<User>(context), Corebyte_platform.IAM.Domain.Repositories.IUserRepository
{
    /**
     * <summary>
     *     Find a user by username
     * </summary>
     * <param name="username">The username to search</param>
     * <returns>The user</returns>
     */
    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.Username.Equals(username));
    }

    /**
     * <summary>
     *     Check if a user exists by username
     * </summary>
     * <param name="username">The username to search</param>
     * <returns>True if the user exists, false otherwise</returns>
     */
    public bool ExistsByUsername(string username)
    {
        return Context.Set<User>().Any(user => user.Username.Equals(username));
    }

    /**
     * <summary>
     *     List all users
     * </summary>
     * <returns>A list of users</returns>
     */
    public async Task<IEnumerable<User>> ListAsync()
    {
        return await Context.Set<User>().ToListAsync();
    }

    /**
     * <summary>
     *     Add a new user
     * </summary>
     * <param name="user">The user to add</param>
     * <returns>Task representing the asynchronous operation</returns>
     */
    public async Task AddAsync(User user)
    {
        await Context.Set<User>().AddAsync(user);
    }

    /**
     * <summary>
     *     Check if a user exists by username asynchronously
     * </summary>
     * <param name="username">The username to check</param>
     * <returns>True if the user exists, false otherwise</returns>
     */
    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        return await Context.Set<User>().AnyAsync(user => user.Username.Equals(username));
    }
}