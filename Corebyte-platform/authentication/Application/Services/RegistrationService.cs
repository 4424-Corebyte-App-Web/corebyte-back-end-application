using System;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;
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
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return (success: false, message: "Todos los campos son obligatorios");

            // Validate email format
            if (!email.Contains("@") || !email.Contains("."))
                return (success: false, message: "El formato del correo electrónico no es válido");

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

            // Hash the password
            string passwordHash = BC.HashPassword(password, BC.GenerateSalt(12));

            // Create new user
            var user = new User
            {
                Username = username.Trim(),
                Email = email.Trim().ToLower(),
                Password = passwordHash
            };

            await _userRepository.AddAsync(user);
            
            return (success: true, message: "Usuario registrado exitosamente");
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"Error al registrar usuario: {ex}");
            return (success: false, message: "Ocurrió un error al registrar el usuario");
        }
    }
}