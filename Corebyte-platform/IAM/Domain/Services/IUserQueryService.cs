using Corebyte_platform.IAM.Domain.Model.Aggregates;
// Using fully qualified names to avoid ambiguity

namespace Corebyte_platform.IAM.Domain.Services;

/**
 * <summary>
 *     The user query service interface
 * </summary>
 * <remarks>
 *     This service contract specifies handling behavior used to query users
 * </remarks>
 */
public interface IUserQueryService
{
    /**
     * <summary>
     *     Handle get user by id query
     * </summary>
     * <param name="query">The get user by id query</param>
     * <returns>The user if found, null otherwise</returns>
     */
    Task<User?> Handle(Corebyte_platform.IAM.Domain.Model.Queries.GetUserByIdQuery query);

    /**
     * <summary>
     *     Handle get all users query
     * </summary>
     * <param name="query">The get all users query</param>
     * <returns>The list of users</returns>
     */
    Task<IEnumerable<User>> Handle(Corebyte_platform.IAM.Domain.Model.Queries.GetAllUsersQuery query);
    
    /**
     * <summary>
     *     Handle get user by username query
     * </summary>
     * <param name="query">The get user by username query</param>
     * <returns>The user if found, null otherwise</returns>
     */
    Task<User?> Handle(Corebyte_platform.IAM.Domain.Model.Queries.GetUserByUsernameQuery query);
}