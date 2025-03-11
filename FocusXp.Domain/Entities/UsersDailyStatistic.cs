using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FocusXp.Domain.Interfaces;

namespace FocusXp.Domain.Entities;

[Table("user_daily_statistics")]
public class UsersDailyStatistic : IEntity, ICreatableEntity
{
    [Required] [Column("user_id")] public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))] public virtual User? User { get; set; }

    [Required] [Column("date")] public DateOnly Date { get; set; }

    [Column("completed_pomodoro_count")]
    [Range(0, int.MaxValue, ErrorMessage = "Completed pomodoro count cannot be negative.")]
    public int CompletedPomodoroCount { get; set; }

    [Column("completed_task_count")]
    [Range(0, int.MaxValue, ErrorMessage = "Completed task count cannot be negative.")]
    public int CompletedTaskCount { get; set; }

    [Column("earned_xp")]
    [Range(0, int.MaxValue, ErrorMessage = "Earned XP cannot be negative.")]
    public int EarnedXp { get; set; }

    [Column("focus_time_minutes")]
    [Range(0, int.MaxValue, ErrorMessage = "Focus time cannot be negative.")]
    public int FocusTimeMinutes { get; set; }

    [Column("streak_count")]
    [Range(0, int.MaxValue, ErrorMessage = "Streak count cannot be negative.")]
    public int StreakCount { get; set; } = 0;

    [Column("max_focus_time_minutes")]
    [Range(0, int.MaxValue, ErrorMessage = "Max focus time cannot be negative.")]
    public int MaxFocusTimeMinutes { get; set; } = 0;

    [Column("is_best_day")] public bool IsBestDay { get; set; } = false;

    [Column("average_focus_time_per_pomodoro")]
    [Range(0, int.MaxValue, ErrorMessage = "Average focus time must be positive.")]
    public int AverageFocusTimePerPomodoro { get; set; } = 0;

    [Column("created_at")] public DateTime CreatedAt { get; init; }

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; } = Guid.NewGuid();
}