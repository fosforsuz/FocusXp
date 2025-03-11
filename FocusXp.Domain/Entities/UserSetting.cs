using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FocusXp.Domain.Enum;
using FocusXp.Domain.Interfaces;

namespace FocusXp.Domain.Entities;

[Table("user_settings")]
public class UserSetting : IEntity, IUpdatableEntity
{
    [Column("user_id")] [Required] public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))] public virtual User? User { get; set; }

    [Column("theme")] public Theme Theme { get; set; } = Theme.Light;

    [Column("pomodoro_duration")]
    [Range(1, 180, ErrorMessage = "Pomodoro duration must be between 1 and 180 minutes.")]
    public int PomodoroDuration { get; set; } = 25;

    [Column("short_break_duration")]
    [Range(1, 60, ErrorMessage = "Short break duration must be between 1 and 60 minutes.")]
    public int ShortBreakDuration { get; set; } = 5;

    [Column("long_break_duration")]
    [Range(1, 60, ErrorMessage = "Long break duration must be between 1 and 60 minutes.")]
    public int LongBreakDuration { get; set; } = 15;

    [Column("pomodoros_until_long_break")]
    [Range(1, 10, ErrorMessage = "Pomodoros until long break must be between 1 and 10.")]
    public int PomodorosUntilLongBreak { get; set; } = 4;

    [Column("auto_start_breaks")] public bool AutoStartBreaks { get; set; } = true;

    [Column("auto_start_next_pomodoro")] public bool AutoStartNextPomodoro { get; set; } = true;

    [Column("auto_start_next_break")] public bool AutoStartNextBreak { get; set; } = true;

    [Column("auto_start_next_long_break")] public bool AutoStartNextLongBreak { get; set; } = true;

    [Column("enable_notifications")] public bool EnableNotifications { get; set; } = true;

    [Column("notification_sound")]
    [StringLength(255, ErrorMessage = "Notification sound name must be at most 255 characters.")]
    public string NotificationSound { get; set; } = "default";

    [Column("notification_volume")]
    [Range(0, 100, ErrorMessage = "Notification volume must be between 0 and 100.")]
    public int NotificationVolume { get; set; } = 50;

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; init; } = Guid.CreateVersion7();

    [Column("updated_at")] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}