using Corebyte_platform.authentication.Domain.Repositories;
using BC = BCrypt.Net.BCrypt;

namespace Corebyte_platform.authentication.Application.Services;

public class LoginService
{
    private readonly IUserRepository _userRepository;

    public LoginService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    /// <summary>
    /// Validates if the username and password are correct.
    /// </summary>
    public async Task<bool> ValidateUserAsync(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            return false;
            
        var user = await _userRepository.GetUserByUsername(username);
        if (user == null)
            return false;
            
        return BC.Verify(password, user.Password);
    }
}