using Corebyte_platform.authentication.Domain.Model.Aggregates;
using Corebyte_platform.authentication.Domain.Repositories;

namespace Corebyte_platform.authentication.Application.Services
{
    public class RegistrationService
    {
        private readonly IUserRepository _userRepository;

        public RegistrationService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<(bool success, string message)> RegisterUserAsync(string username, string email, string password, string rol)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByUsername(username);
                if (existingUser != null)
                    return (false, "El nombre de usuario ya está en uso");

                var existingEmail = await _userRepository.FindByEmailAsync(email);
                if (existingEmail != null)
                    return (false, "El correo electrónico ya está registrado");

                var user = new User
                {
                    Username = username,
                    Email = email,
                    Password = password,
                    Rol = rol
                };

                await _userRepository.AddAsync(user);

                return (true, "Usuario registrado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar usuario: {ex.Message}");
                return (false, "Ocurrió un error al registrar el usuario");
            }
        }
    }
}
