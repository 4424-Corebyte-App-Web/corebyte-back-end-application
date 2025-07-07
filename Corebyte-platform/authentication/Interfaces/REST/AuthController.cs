using Corebyte_platform.authentication.Application.Services;
using Corebyte_platform.authentication.Domain.Repositories;
using Corebyte_platform.authentication.Interfaces.REST.Resources;
using Corebyte_platform.IAM.Application.Internal.OutboundServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Corebyte_platform.authentication.Interfaces.REST;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly LoginService _loginService;
    private readonly RegistrationService _registrationService;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthController(
        LoginService loginService, 
        RegistrationService registrationService,
        IUserRepository userRepository,
        ITokenService tokenService)
    {
        _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
        _registrationService = registrationService ?? throw new ArgumentNullException(nameof(registrationService));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var isValid = await _loginService.ValidateUserAsync(request.Username, request.Password);

        if (!isValid)
            return Unauthorized(new { message = "Credenciales incorrectas" });
            
        var user = await _userRepository.GetUserByUsername(request.Username);
        if (user == null)
            return Unauthorized(new { message = "Usuario no encontrado" });
            
        var token = _tokenService.GenerateToken(new Corebyte_platform.IAM.Domain.Model.Aggregates.User(user.Username, user.Password));
        
        return Ok(new 
        { 
            message = "Login exitoso",
            token,
            user = new 
            {
                id = user.Id,
                username = user.Username,
                email = user.Email
            }
        });
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