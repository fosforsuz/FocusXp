using FocusXp.Infrastructure.Model;

namespace FocusXp.Infrastructure.Interfaces.Security;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(string email, string password, CancellationToken cancellationToken);
}