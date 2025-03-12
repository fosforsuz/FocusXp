using FluentResults;
using FocusXp.Domain.Enum;
using FocusXp.Infrastructure.Model;
using MediatR;

namespace FocusXp.Application.Command;

public class CreateUserCommand : IRequest<Result<AuthResponse>>
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.User;
}