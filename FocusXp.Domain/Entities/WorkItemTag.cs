using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FocusXp.Domain.Interfaces;

namespace FocusXp.Domain.Entities;

[Table("work_item_tags")]
public class WorkItemTag : IEntity, ICreatableEntity, IUpdatableEntity
{
    [Required] [Column("work_item_id")] public Guid WorkItemId { get; init; }

    [ForeignKey(nameof(WorkItemId))] public virtual WorkItem? WorkItem { get; init; }

    [Required] [Column("tag-id")] public Guid TagId { get; init; }
    [ForeignKey("TagId")] public virtual Tag? Tag { get; init; }

    [Column("created-at")] public DateTime CreatedAt { get; init; } = DateTime.UtcNow;


    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; init; }

    [Column("updated-at")] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}