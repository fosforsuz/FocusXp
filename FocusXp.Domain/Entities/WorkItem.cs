using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FocusXp.Domain.Enum;
using FocusXp.Domain.Interfaces;

namespace FocusXp.Domain.Entities;

[Table("work_items")]
public class WorkItem : ITrackableEntity
{
    [Required] [Column("user_id")] public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))] public virtual User? User { get; set; }

    [Required]
    [Column("title")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters.")]
    public required string Title { get; set; }

    [Column("description")]
    [StringLength(4000, ErrorMessage = "Description must be at most 4000 characters long.")]
    public string? Description { get; set; }

    [Column("priority")] public Priority Priority { get; set; } = Priority.Medium;

    [Column("task_status")] public WorkItemStatus TaskStatus { get; set; } = WorkItemStatus.InProgress;

    [Column("estimated_pomodoros")]
    [Range(1, 100, ErrorMessage = "Estimated pomodoros must be between 1 and 100.")]
    public int? EstimatedPomodoros { get; set; }

    [Column("due_date")] public DateTime? DueDate { get; set; }

    [Column("is_completed")] public bool IsCompleted { get; set; }
    [Column("completed_at")] public DateTime? CompletedAt { get; set; }

    [Column("recurrence_type")] public RecurrenceType RecurrenceType { get; set; } = RecurrenceType.None;

    [Column("time_spent")]
    [Range(0, int.MaxValue)]
    public int TimeSpent { get; set; } = 0;

    public virtual ICollection<WorkItemTag> WorkItemTags { get; set; } = new List<WorkItemTag>();
    public virtual ICollection<SubWorkItem> SubWorkItems { get; set; } = new List<SubWorkItem>();
    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    public virtual ICollection<PomodoroSession> PomodoroSessions { get; set; } = new List<PomodoroSession>();


    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; init; } = Guid.CreateVersion7();

    [Column("created_at")] public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    [Column("updated_at")] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    [Column("is_deleted")] public bool IsDeleted { get; set; } = false;
}