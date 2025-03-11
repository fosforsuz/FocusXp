using FocusXp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusXp.Infrastructure.Mapping;

public class AttachmentMapping : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.Property(attachment => attachment.Id).ValueGeneratedNever();

        builder.HasOne(a => a.WorkItem)
            .WithMany(w => w.Attachments)
            .HasForeignKey(a => a.WorkItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(a => a.FileUrl)
            .HasDatabaseName("IX_Attachments_FileUrl");

        builder.HasIndex(a => a.WorkItemId)
            .HasDatabaseName("IX_Attachments_WorkItemId");
    }
}