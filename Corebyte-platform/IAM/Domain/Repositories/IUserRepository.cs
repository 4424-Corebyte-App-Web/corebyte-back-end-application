

using Corebyte_platform.IAM.Domain.Model.Aggregates;
using Corebyte_platform.Shared.Domain.Repositories;

namespace Corebyte_platform.IAM.Domain.Repositories;

/**
 * <summary>
 *     The user repository
 * </summary>
 * <remarks>
 *     This repository is used to manage users
 * </remarks>
 */
public interface IUserRepository : IBaseRepository<User>
{
    /**
     * <summary>
     *     Find a user by id
     * </summary>
     * <param name="username">The username to search</param>
     * <returns>The user</returns>
     */
    Task<User?> FindByUsernameAsync(string username);

    /**
     * <summary>
     *     Check if a user exists by username
     * </summary>
     * <param name="username">The username to search</param>
     * <returns>True if the user exists, false otherwise</returns>
     */
    bool ExistsByUsername(string username);
    
    /**
     * <summary>
     *     List all users
     * </summary>
     * <returns>A list of users</returns>
     */
    Task<IEnumerable<User>> ListAsync();
    
    /**
     * <summary>
     *     Add a new user
     * </summary>
     * <param name="user">The user to add</param>
     * <returns>Task representing the asynchronous operation</returns>
     */
    Task AddAsync(User user);
    
    /**
     * <summary>
     *     Check if a user exists by username asynchronously
     * </summary>
     * <param name="username">The username to check</param>
     * <returns>True if the user exists, false otherwise</returns>
     */
    Task<bool> ExistsByUsernameAsync(string username);
}