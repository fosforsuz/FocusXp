using FluentResults;
using FocusXp.Infrastructure.Model;
using MediatR;

namespace FocusXp.Application.Command;

public class LoginCommand : IRequest<Result<AuthResponse>>
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}