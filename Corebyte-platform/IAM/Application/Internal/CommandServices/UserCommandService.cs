using Corebyte_platform.IAM.Domain.Model.Aggregates;
using Corebyte_platform.IAM.Domain.Repositories;
using Corebyte_platform.IAM.Application.Internal.OutboundServices;
using Corebyte_platform.IAM.Domain.Model.Commands;
using Corebyte_platform.IAM.Domain.Services;
using Corebyte_platform.Shared.Domain.Repositories;

namespace Corebyte_platform.IAM.Application.Internal.CommandServices;

/**
 * <summary>
 *     The user command service
 * </summary>
 * <remarks>
 *     This class is used to handle user commands
 * </remarks>
 */
public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork)
    : IUserCommandService
{
    public IHashingService HashingService { get; } = hashingService;

    /**
* <summary>
*     Handle sign in command
* </summary>
* <param name="command">The sign in command</param>
* <returns>The authenticated user and the JWT token</returns>
*/
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username);

        if (user == null || !HashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid username or password");

        var token = tokenService.GenerateToken(user);

        return (user, token);
    }

    /**
     * <summary>
     *     Handle sign up command
     * </summary>
     * <param name="command">The sign up command</param>
     * <returns>A confirmation message on successful creation.</returns>
     */
    public async Task Handle(SignUpCommand command)
    {
        if (await userRepository.ExistsByUsernameAsync(command.Username))
            throw new Exception($"Username {command.Username} is already taken");

        var user = new User()
            .UpdateUsername(command.Username)
            .UpdatePasswordHash(HashingService.HashPassword(command.Password));
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating user: {e.Message}");
        }
    }
}