using FocusXp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusXp.Infrastructure.Mapping;

public class WorkItemMapping : IEntityTypeConfiguration<WorkItem>
{
    public void Configure(EntityTypeBuilder<WorkItem> builder)
    {
        builder.Property(item => item.Id)
            .ValueGeneratedNever();

        builder.HasOne(item => item.User)
            .WithMany(user => user.WorkItems)
            .HasForeignKey(item => item.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(item => item.UserId)
            .HasDatabaseName("IX_work_items_user_id");
    }
}