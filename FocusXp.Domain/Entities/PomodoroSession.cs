using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FocusXp.Domain.Enum;
using FocusXp.Domain.Interfaces;

namespace FocusXp.Domain.Entities;

[Table("pomodoro_sessions")]
public class PomodoroSession : IEntity
{
    [Required] [Column("user_id")] public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))] public virtual User? User { get; set; }

    [Column("work_item_id")] public Guid? WorkItemId { get; set; }
    [ForeignKey(nameof(WorkItemId))] public virtual WorkItem? WorkItem { get; set; }

    [Column("start_time")] public DateTime StartTime { get; set; } = DateTime.UtcNow;

    [Column("end_time")] public DateTime? EndTime { get; set; }

    [Column("duration_minutes")]
    [Range(0, int.MaxValue, ErrorMessage = "Duration must be at least 0 minutes.")]
    public int DurationMinutes { get; set; } = 0;

    [Column("is_completed")] public bool IsCompleted { get; set; }

    [Column("interruption_count")]
    [Range(0, int.MaxValue, ErrorMessage = "Interruption count cannot be negative.")]
    public int InterruptionCount { get; set; } = 0;

    [Column("session_type")] public SessionType SessionType { get; set; } = SessionType.Pomodoro;

    [Column("focus_level")] public FocusLevel FocusLevel { get; set; } = FocusLevel.Medium;

    [Column("session_rating")]
    [Range(1, 5, ErrorMessage = "Session rating must be between 1 and 5.")]
    public int? SessionRating { get; set; }


    [Column("goal_achieved")] public bool? GoalAchieved { get; set; }

    public virtual ICollection<PomodoroDistraction> Distractions { get; set; } = new List<PomodoroDistraction>();


    [Column("notes")]
    [StringLength(500, ErrorMessage = "Notes cannot be longer than 500 characters.")]
    public string? Notes { get; set; }

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; } = Guid.CreateVersion7();
}