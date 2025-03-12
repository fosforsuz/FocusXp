using FluentResults;
using MediatR;

namespace FocusXp.Application.Command;

public class UpdateUserCommand : IRequest<Result<Unit>>
{
    public Guid UserId { get; private set; }
    public void SetUserId(Guid userId) => UserId = userId;
    public string Username { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Fullname { get; init; } = null!;
}
