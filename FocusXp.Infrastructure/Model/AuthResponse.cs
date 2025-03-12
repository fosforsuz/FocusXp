namespace FocusXp.Infrastructure.Model;

public class AuthResponse
{
    public AuthResponse(bool authenticateResult, string? token, DateTime expiresAt, bool isTwoFactorEnabled)
    {
        AuthenticateResult = authenticateResult;
        Token = token;
        ExpiresAt = expiresAt;
        IsTwoFactorEnabled = isTwoFactorEnabled;
    }

    public bool IsTwoFactorEnabled { get; set; }

    public bool AuthenticateResult { get; }
    public string? Token { get; }
    public DateTime ExpiresAt { get; }
}