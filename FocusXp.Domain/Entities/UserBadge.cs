using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FocusXp.Domain.Interfaces;

namespace FocusXp.Domain.Entities;

[Table("user_badges")]
public class UserBadge : IEntity
{
    [Required] [Column("user_id")] public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))] public virtual User? User { get; set; }

    [Required] [Column("badge_id")] public Guid BadgeId { get; set; }

    [ForeignKey(nameof(BadgeId))] public virtual Badge? Badge { get; set; }

    [Column("earned_at")] public DateTime EarnedAt { get; set; } = DateTime.UtcNow;


    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; } = Guid.CreateVersion7();
}