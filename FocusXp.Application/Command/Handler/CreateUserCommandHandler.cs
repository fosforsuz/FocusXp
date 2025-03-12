using FluentResults;
using FocusXp.Application.Interface;
using FocusXp.Domain.Extensions;
using FocusXp.Infrastructure.Interfaces.Security;
using FocusXp.Infrastructure.Model;
using MediatR;

namespace FocusXp.Application.Command.Handler;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<AuthResponse>>
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public CreateUserCommandHandler(IUserService userService, ITokenService tokenService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async Task<Result<AuthResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userResult = await _userService.CreateUserAsync(request, cancellationToken);
        if (userResult.IsFailed)
            return Result.Fail<AuthResponse>(userResult.Errors);

        var user = userResult.ValueOrDefault;
        if (user is null)
            return Result.Fail<AuthResponse>("User creation failed but no error details available.");

        var token = await _tokenService.GenerateToken(
            id: user.Id,
            email: user.Email,
            name: user.Fullname,
            roleName: user.Role.GetRoleString()
        );

        var authResponse = new AuthResponse(
            authenticateResult: true,
            token: token.Token,
            expiresAt: token.ExpiresAt,
            isTwoFactorEnabled: false
        );

        return Result.Ok(authResponse);
    }
}