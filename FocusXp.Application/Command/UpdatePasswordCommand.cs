using FluentResults;
using MediatR;

namespace FocusXp.Application.Command;

public class UpdatePasswordCommand : IRequest<Result<Unit>>
{
    public Guid UserId { get; set; }
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}