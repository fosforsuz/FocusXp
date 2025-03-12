namespace FocusXp.Application.Command;

public class UpdatePasswordCommand
{
    public Guid UserId { get; set; }
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}