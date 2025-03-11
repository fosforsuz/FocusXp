using FocusXp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusXp.Infrastructure.Mapping;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.Id)
            .ValueGeneratedNever();

        builder.HasIndex(user => user.Username)
            .IsUnique()
            .HasDatabaseName("users_username_unique");

        builder.HasIndex(user => user.Email)
            .IsUnique()
            .HasDatabaseName("users_email_unique");

        builder.Property(user => user.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnUpdate();
    }
}