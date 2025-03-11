using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FocusXp.Domain.Enum;
using FocusXp.Domain.Interfaces;

namespace FocusXp.Domain.Entities;

[Table("pomodoro_distractions")]
public class PomodoroDistraction : IEntity, ICreatableEntity
{
    [Required] [Column("session_id")] public Guid PomodoroSessionId { get; set; }

    [ForeignKey(nameof(PomodoroSessionId))]
    public virtual PomodoroSession? PomodoroSession { get; set; }

    [Required]
    [Column("distraction_type")]
    public DistractionType DistractionType { get; set; }

    [Column("created_at")] public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; } = Guid.CreateVersion7();
}