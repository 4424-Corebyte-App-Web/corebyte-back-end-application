using Corebyte_platform.authentication.Application.Services;
using Corebyte_platform.authentication.Domain.Repositories;
using Corebyte_platform.authentication.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Corebyte_platform.authentication.Interfaces.REST
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginService _loginService;
        private readonly RegistrationService _registrationService;
        private readonly IUserRepository _userRepository;

        public AuthController(
            LoginService loginService,
            RegistrationService registrationService,
            IUserRepository userRepository)
        {
            _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
            _registrationService = registrationService ?? throw new ArgumentNullException(nameof(registrationService));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Esperamos una tupla de 5 valores
            var result = await _loginService.LoginAndGenerateTokenAsync(request.Username, request.Password);

            if (!result.success)
                return Unauthorized(new { message = "Credenciales incorrectas" });

            return Ok(new
            {
                token = result.token,
                user = new
                {
                    username = result.username,
                    email = result.email,
                    role = result.rol
                }
            });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request.Password != request.ConfirmPassword)
                return BadRequest(new { message = "Las contraseñas no coinciden" });

            var result = await _registrationService.RegisterUserAsync(
                request.Username,
                request.Email,
                request.Password,
                request.Rol);

            if (result.success)
                return Ok(new { message = result.message });

            return BadRequest(new { message = result.message });
        }
    }
}
