using FocusXp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusXp.Infrastructure.Mapping;

public class TagMapping : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(tag => tag.Id)
            .ValueGeneratedNever();

        builder.HasOne(tag => tag.User)
            .WithMany(user => user.Tags)
            .HasForeignKey(tag => tag.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(tag => tag.UserId)
            .HasDatabaseName("IX_tags_user_id");

        builder.HasIndex(tag => new { tag.UserId, tag.Name })
            .IsUnique()
            .HasDatabaseName("IX_tags_user_id_name");
        
        builder.Property(tag => tag.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnUpdate();
    }
}