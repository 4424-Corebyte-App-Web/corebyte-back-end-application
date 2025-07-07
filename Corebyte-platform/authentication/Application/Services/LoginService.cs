using Corebyte_platform.authentication.Domain.Repositories;

namespace Corebyte_platform.authentication.Application.Services;

public class LoginService
{
    private readonly IUserRepository _userRepository;

    public LoginService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Valida si el usuario y contraseña son correctos.
    /// </summary>
    public async Task<bool> ValidateUserAsync(string username, string password)
    {
        var user = await _userRepository.GetUserByUsername(username);
        return user != null && user.Password == password;
    }
}