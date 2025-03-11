using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FocusXp.Domain.Interfaces;

namespace FocusXp.Domain.Entities;

[Table("attachments")]
public class Attachment : IEntity, ICreatableEntity
{
    [Required] [Column("work-item-id")] public Guid WorkItemId { get; set; }

    [ForeignKey(nameof(WorkItemId))] public virtual WorkItem? WorkItem { get; set; }

    [Required]
    [Column("file-url")]
    [StringLength(500, ErrorMessage = "File-url cannot exceed 500 characters")]
    public required string FileUrl { get; set; }

    [Column("created-at")] public DateTime CreatedAt { get; init; } = DateTime.UtcNow;


    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; init; } = Guid.CreateVersion7();
}