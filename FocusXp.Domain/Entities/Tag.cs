using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FocusXp.Domain.Interfaces;

namespace FocusXp.Domain.Entities;

[Table("tags")]
public class Tag : ITrackableEntity
{
    [Required] [Column("user_id")] public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))] public virtual User? User { get; set; }

    [Required]
    [Column("name")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Tag name must be between 3 and 50 characters")]
    public required string Name { get; set; }

    [Column("color")]
    [RegularExpression(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Invalid color format.")]
    [StringLength(20, ErrorMessage = "Color must be at most 20 characters long.")]
    public string? Color { get; set; }

    public virtual ICollection<WorkItem> WorkItems { get; set; } = new HashSet<WorkItem>();

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; init; } = Guid.CreateVersion7();

    [Column("created_at")] public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    [Column("updated_at")] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [Column("deleted_at")] public bool IsDeleted { get; set; }
}