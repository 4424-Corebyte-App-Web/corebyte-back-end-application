using Corebyte_platform.authentication.Application.Services;
using Corebyte_platform.authentication.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Corebyte_platform.authentication.Interfaces.REST;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly LoginService _loginService;
    private readonly RegistrationService _registrationService;

    public AuthController(LoginService loginService, RegistrationService registrationService)
    {
        _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
        _registrationService = registrationService ?? throw new ArgumentNullException(nameof(registrationService));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var isValid = await _loginService.ValidateUserAsync(request.Username, request.Password);

        if (isValid)
            return Ok(new { message = "Login exitoso" });
        else
            return Unauthorized(new { message = "Credenciales incorrectas" });
    }

    /// <summary>
    /// Registra un nuevo usuario en el sistema
    /// </summary>
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
            request.Password);

        if (result.success)
            return Ok(new { message = result.message });
        
        return BadRequest(new { message = result.message });
    }
}