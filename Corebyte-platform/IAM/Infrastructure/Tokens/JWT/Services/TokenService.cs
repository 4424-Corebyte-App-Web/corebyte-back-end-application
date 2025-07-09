
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Corebyte_platform.IAM.Infrastructure.Tokens.JWT.Configuration;
using Corebyte_platform.IAM.Application.Internal.OutboundServices;
using Corebyte_platform.IAM.Domain.Model.Aggregates;

namespace Corebyte_platform.IAM.Infrastructure.Tokens.JWT.Services;

/**
 * <summary>
 *     The token service
 * </summary>
 * <remarks>
 *     This class is used to generate and validate tokens
 * </remarks>
 */
public class TokenService(IOptions<TokenSettings> tokenSettings) : ITokenService
{
    private readonly TokenSettings _tokenSettings = tokenSettings.Value;

    /**
     * <summary>
     *     Generate token
     * </summary>
     * <param name="user">The user for token generation</param>
     * <returns>The generated Token</returns>
     */
    public string GenerateToken(User user)
    {
        try
        {
            Console.WriteLine($"[TokenService] Starting token generation for user ID: {user?.Id}, Username: {user?.Username}");
            
            if (string.IsNullOrWhiteSpace(_tokenSettings?.Secret))
            {
                var errorMsg = "JWT Secret is not configured";
                Console.WriteLine($"[TokenService] Error: {errorMsg}");
                throw new InvalidOperationException(errorMsg);
            }

            if (user == null)
            {
                var errorMsg = "User cannot be null";
                Console.WriteLine($"[TokenService] Error: {errorMsg}");
                throw new ArgumentNullException(nameof(user));
            }

            if (user.Id <= 0)
            {
                var errorMsg = $"Invalid user ID: {user.Id}";
                Console.WriteLine($"[TokenService] Error: {errorMsg}");
                throw new ArgumentException(errorMsg, nameof(user.Id));
            }

            Console.WriteLine($"[TokenService] Creating token descriptor for user ID: {user.Id}");
            var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username ?? string.Empty)
            };

            Console.WriteLine($"[TokenService] Claims created: {string.Join(", ", claims.Select(c => $"{c.Type}: {c.Value}"))}");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            Console.WriteLine("[TokenService] Creating token...");
            var tokenHandler = new JsonWebTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            Console.WriteLine("[TokenService] Token generated successfully");
            return token;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[TokenService] Error generating token: {ex}");
            Console.WriteLine($"[TokenService] Stack trace: {ex.StackTrace}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"[TokenService] Inner exception: {ex.InnerException}");
            }
            throw new Exception("Failed to generate authentication token. See logs for details.", ex);
        }
    }

    /**
     * <summary>
     *     VerifyPassword token
     * </summary>
     * <param name="token">The token to validate</param>
     * <returns>The user id if the token is valid, null otherwise</returns>
     */
    public async Task<int?> ValidateToken(string token)
    {
        // If token is null or empty
        if (string.IsNullOrEmpty(token))
            // Return null 
            return null;
        // Otherwise, perform validation
        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
        try
        {
            var tokenValidationResult = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // Expiration without delay
                ClockSkew = TimeSpan.Zero
            });

            var jwtToken = (JsonWebToken)tokenValidationResult.SecurityToken;
            var userId = int.Parse(jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value);
            return userId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}