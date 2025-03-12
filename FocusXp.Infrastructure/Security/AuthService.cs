using FocusXp.Domain.Entities;
using FocusXp.Domain.Exceptions;
using FocusXp.Domain.Extensions;
using FocusXp.Infrastructure.Interfaces.Data;
using FocusXp.Infrastructure.Interfaces.Security;
using FocusXp.Infrastructure.Model;

namespace FocusXp.Infrastructure.Security;

internal class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> LoginAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetSingleAsync(
            user => user.Email == email && !user.IsDeleted,
            user => new User
            {
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Fullname = user.Fullname,
                Role = user.Role
            },
            cancellationToken: cancellationToken);

        if (user is null)
            throw new InvalidEmailOrPasswordException();

        if (!user.CheckPassword(password))
            throw new InvalidEmailOrPasswordException();

        var token = await _tokenService.GenerateToken(user.Id, user.Email, user.Fullname,
            user.Role.GetRoleString());

        return new AuthResponse(true, token.Token, token.ExpiresAt, false);
    }

    public async Task<AuthResponse> LoginAsync(Guid id, string email, string fullname, string roleName)
    {
        var token = await _tokenService.GenerateToken(id, email, fullname, roleName);

        return new AuthResponse(true, token.Token, token.ExpiresAt, false);
    }
}