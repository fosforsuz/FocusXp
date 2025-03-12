using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FocusXp.Infrastructure.Config;
using FocusXp.Infrastructure.Interfaces.Security;
using FocusXp.Infrastructure.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FocusXp.Infrastructure.Service;

internal class TokenService(IOptions<JwtSettings> jwtSettings) : ITokenService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public Task<TokenResponse> GenerateToken(Guid id, string email, string name, string roleName)
    {
        var symmetricSecurityKey =
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.SecretKey));

        var datetimeNow = DateTime.UtcNow;
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Role, roleName),
            new(ClaimTypes.Name, name),
            new(ClaimTypes.NameIdentifier, id.ToString())
        };

        var jwt = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            datetimeNow,
            datetimeNow.Add(TimeSpan.FromMinutes(_jwtSettings.ExpirationMinutes)),
            new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return Task.FromResult(new TokenResponse(token,
            datetimeNow.Add(TimeSpan.FromMinutes(_jwtSettings.ExpirationMinutes))));
    }
}