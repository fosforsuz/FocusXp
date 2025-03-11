using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FocusXp.Domain.Interfaces;

namespace FocusXp.Domain.Entities;

[Table("sub_work_items")]
public class SubWorkItem : ITrackableEntity
{
    [Required] [Column("work_item_id")] public Guid WorkItemId { get; set; }
    [ForeignKey(nameof(WorkItemId))] public virtual WorkItem? WorkItem { get; set; }

    [Required]
    [Column("title")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "SubTask title must be between 3 and 100 characters.")]
    public required string Title { get; set; }

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; init; } = Guid.CreateVersion7();

    [Column("created_at")] public DateTime CreatedAt { get; init; }
    [Column("updated_at")] public DateTime UpdatedAt { get; set; }
    [Column("deleted_at")] public bool IsDeleted { get; set; }
}