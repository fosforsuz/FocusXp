using FocusXp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusXp.Infrastructure.Mapping;

public class WorkItemTagMapping : IEntityTypeConfiguration<WorkItemTag>
{
    public void Configure(EntityTypeBuilder<WorkItemTag> builder)
    {
        builder.Property(tag => tag.Id)
            .ValueGeneratedNever();

        builder.HasOne(tag => tag.WorkItem)
            .WithMany(item => item.WorkItemTags)
            .HasForeignKey(tag => tag.WorkItemId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(tag => tag.WorkItemId)
            .HasDatabaseName("idx_work_item_tags_work_item_id");

        builder.HasIndex(tag => new { tag.WorkItemId, tag.TagId })
            .IsUnique()
            .HasDatabaseName("idx_work_item_tags_work_item_id_tag_id");

        builder.HasOne(tag => tag.Tag)
            .WithMany(tag => tag.WorkItemTags)
            .HasForeignKey(tag => tag.TagId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(tag => tag.TagId)
            .HasDatabaseName("idx_work_item_tags_tag_id");

        builder.Property(tag => tag.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnUpdate();
    }
}