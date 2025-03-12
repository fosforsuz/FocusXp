using FocusXp.Infrastructure.Model;

namespace FocusXp.Infrastructure.Interfaces.Security;

public interface ITokenService
{
    Task<TokenResponse> GenerateToken(Guid id, string email, string name, string roleName);
}