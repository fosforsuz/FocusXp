namespace FocusXp.Application.Dto;

public class UserDto
{
    public Guid Id { get; init; }
    public string Email { get; init; } = null!;
    public string Username { get; init; } = null!;
    public string Fullname { get; init; } = null!;
    public int XpPoints { get; init; }
    public int Level { get; init; }
    public string Role { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
    public DateTime ModifiedAt { get; init; }
}