using Corebyte_platform.authentication.Domain.Model.Aggregates;
using Corebyte_platform.authentication.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Corebyte_platform.authentication.Application.Services;

public class LoginService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public LoginService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    /// <summary>
    /// Valida las credenciales y genera un token si son correctas.
    /// </summary>
    public async Task<(bool success, string token, string username, string email, string rol)> LoginAndGenerateTokenAsync(string username, string password)
    {
        var user = await _userRepository.GetUserByUsername(username);

        if (user == null || user.Password != password)
        {
            return (false, null, null, null, null);
        }

        // Crear claims
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Rol)
        };

        // Obtener la clave secreta del appsettings.json
        var secretKey = _configuration["Jwt:Key"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Crear el token
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return (true, tokenString, user.Username, user.Email, user.Rol);
    }
}
