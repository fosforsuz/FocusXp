namespace FocusXp.Infrastructure.Model;

public class TokenResponse(string token, DateTime expiresAt)
{
    public string Token { get; } = token;
    public DateTime ExpiresAt { get; } = expiresAt;
}