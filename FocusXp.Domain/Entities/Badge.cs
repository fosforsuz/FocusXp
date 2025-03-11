using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FocusXp.Domain.Interfaces;

namespace FocusXp.Domain.Entities;

[Table("badges")]
public class Badge : IEntity, ICreatableEntity
{
    [Required]
    [Column("name")]
    [StringLength(50, ErrorMessage = "Badge name must be at most 50 characters.")]
    public required string Name { get; set; }

    [Required] [Column("description")] public required string Description { get; set; }

    [Required]
    [Column("icon_url")]
    [StringLength(255, ErrorMessage = "Icon URL must be at most 255 characters.")]
    public required string IconUrl { get; set; }

    [Column("xp_reward")] public int XpReward { get; set; } = 0;

    [Required]
    [Column("requirement_type")]
    [StringLength(50, ErrorMessage = "Requirement type must be at most 50 characters.")]
    public required string RequirementType { get; set; }

    [Required]
    [Column("requirement_value")]
    [Range(1, int.MaxValue, ErrorMessage = "Requirement value must be at least 1.")]
    public int RequirementValue { get; set; }


    public virtual ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();

    [Column("created_at")] public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; } = Guid.CreateVersion7();
}