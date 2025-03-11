using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FocusXp.Domain.Enum;
using FocusXp.Domain.Interfaces;

namespace FocusXp.Domain.Entities;

[Table("xp_transactions")]
public class XpTransaction : IEntity, ICreatableEntity
{
    [Required] [Column("user_id")] public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))] public virtual User? User { get; set; }

    [Required]
    [Column("transaction_type")]
    public virtual XpTransactionType TransactionType { get; set; }

    [Column("reference_type")]
    [Required]
    [StringLength(50)]
    public required string ReferenceType { get; set; }

    [Column("reference_id")] public Guid ReferenceId { get; set; }

    [Column("created_at")] public DateTime CreatedAt { get; init; } = DateTime.UtcNow;


    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; } = Guid.CreateVersion7();
}