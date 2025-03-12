using FocusXp.Domain.Entities;

namespace FocusXp.Infrastructure.Model;

public class LoginResult
{
    public LoginResult(Guid userId, AuthResponse authResponse, User user)
    {
        UserId = userId;
        AuthResponse = authResponse;
        User = user;
    }

    public Guid UserId { get; }
    public AuthResponse AuthResponse { get; }
    public User User { get; }
}