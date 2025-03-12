using FluentResults;
using FocusXp.Domain.Exceptions;
using FocusXp.Infrastructure.Interfaces.Security;
using FocusXp.Infrastructure.Model;
using MediatR;

namespace FocusXp.Application.Command.Handler;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResponse>>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    public async Task<Result<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _authService.LoginAsync(request.Email, request.Password, cancellationToken);
            return Result.Ok(result);
        }
        catch (InvalidEmailOrPasswordException e)
        {
            return Result.Fail<AuthResponse>(e.Message);
        }
    }
}