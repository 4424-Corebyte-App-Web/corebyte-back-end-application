using Corebyte_platform.IAM.Domain.Model.Aggregates;
using Corebyte_platform.IAM.Domain.Repositories;
using Corebyte_platform.IAM.Domain.Model.Queries;
using Corebyte_platform.IAM.Domain.Services;
using GetUserByIdQuery = Corebyte_platform.IAM.Domain.Model.Queries.GetUserByIdQuery;
using GetUserByUsernameQuery = Corebyte_platform.IAM.Domain.Model.Queries.GetUserByUsernameQuery;
using GetAllUsersQuery = Corebyte_platform.IAM.Domain.Model.Queries.GetAllUsersQuery;

namespace Corebyte_platform.IAM.Application.Internal.QueryServices;

/**
 * <summary>
 *     The user query service implementation class
 * </summary>
 * <remarks>
 *     This class is used to handle user queries
 * </remarks>
 */
public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    /**
     * <summary>
     *     Handle get user by id query
     * </summary>
     * <param name="query">The query object containing the user id to search</param>
     * <returns>The user</returns>
     */
    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.Id);
    }

    /**
     * <summary>
     *     Handle get user by username query
     * </summary>
     * <param name="query">The query object for getting all users</param>
     * <returns>The user</returns>
     */
    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
    {
        return await userRepository.ListAsync();
    }

    /**
     * <summary>
     *     Handle get user by username query
     * </summary>
     * <param name="query">The query object containing the username to search</param>
     * <returns>The user</returns>
     */
    public async Task<User?> Handle(GetUserByUsernameQuery query)
    {
        return await userRepository.FindByUsernameAsync(query.Username);
    }
}