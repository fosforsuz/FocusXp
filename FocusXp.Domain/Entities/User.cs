using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FocusXp.Domain.Enum;
using FocusXp.Domain.Interfaces;

namespace FocusXp.Domain.Entities;

[Table("users")]
public class User : ITrackableEntity
{
    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters.")]
    [RegularExpression(@"^[\p{L}0-9_]+$",
        ErrorMessage = "Username can only contain letters, numbers, and underscores.")]
    [Column("username")]
    public required string Username { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [StringLength(100, ErrorMessage = "Email must be at most 100 characters long.")]
    [Column("email")]
    public required string Email { get; set; }

    [Required]
    [StringLength(255, MinimumLength = 8, ErrorMessage = "Password hash must be between 8 and 255 characters.")]
    [Column("password_hash")]
    public required string PasswordHash { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Fullname must be between 3 and 100 characters.")]
    [Column("fullname")]
    public required string Fullname { get; set; }

    [Url(ErrorMessage = "Invalid URL format.")]
    [Column("profile_picture_url")]
    public string? ProfilePictureUrl { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "XP points must be zero or greater.")]
    [Column("xp_points")]
    public int XpPoints { get; set; } = 0;

    [Range(1, int.MaxValue, ErrorMessage = "Level must be at least 1.")]
    [Column("level")]
    public int Level { get; set; } = 1;

    [Column("role")] public Role Role { get; set; } = Role.User;

    public virtual UserSetting? UserSetting { get; set; }

    public virtual ICollection<PomodoroSession> PomodoroSessions { get; set; } = new List<PomodoroSession>();

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; init; } = Guid.CreateVersion7();

    [Column("created_at")] public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    [Column("updated_at")] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [Column("is_deleted")] public bool IsDeleted { get; set; }
}