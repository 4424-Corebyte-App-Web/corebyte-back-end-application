using Corebyte_platform.authentication.Domain.Model.Aggregates;
using Corebyte_platform.authentication.Domain.Repositories;

namespace Corebyte_platform.authentication.Application.Services;

public class RegistrationService
{
    private readonly IUserRepository _userRepository;

    public RegistrationService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<(bool success, string message)> RegisterUserAsync(string username, string email, string password)
    {
        try
        {
            // Check if username already exists
            var existingUser = await _userRepository.GetUserByUsername(username);
            if (existingUser != null)
            {
                return (success: false, message: "El nombre de usuario ya está en uso");
            }

            // Check if email already exists
            var existingEmail = await _userRepository.FindByEmailAsync(email);
            if (existingEmail != null)
            {
                return (success: false, message: "El correo electrónico ya está registrado");
            }

            // Create new user
            var user = new User
            {
                Username = username,
                Email = email,
                Password = password // In a real app, this should be hashed
            };

            await _userRepository.AddAsync(user);
            
            return (success: true, message: "Usuario registrado exitosamente");
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"Error al registrar usuario: {ex.Message}");
            return (success: false, message: "Ocurrió un error al registrar el usuario");
        }
    }
}