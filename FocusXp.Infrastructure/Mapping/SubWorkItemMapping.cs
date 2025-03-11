using FocusXp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusXp.Infrastructure.Mapping;

public class SubWorkItemMapping : IEntityTypeConfiguration<SubWorkItem>
{
    public void Configure(EntityTypeBuilder<SubWorkItem> builder)
    {
        builder.Property(item => item.Id)
            .ValueGeneratedNever();

        builder.HasOne(item => item.WorkItem)
            .WithMany(item => item.SubWorkItems)
            .HasForeignKey(item => item.WorkItemId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(item => item.WorkItemId)
            .HasDatabaseName("IX_sub_work_items_work_item_id");
        
        builder.Property(item => item.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnUpdate();
    }
}