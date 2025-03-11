using FocusXp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusXp.Infrastructure.Mapping;

public class BadgeMapping : IEntityTypeConfiguration<Badge>
{
    public void Configure(EntityTypeBuilder<Badge> builder)
    {
        builder.Property(badge => badge.Id).ValueGeneratedNever();

        builder.HasIndex(badge => badge.Name)
            .IsUnique()
            .HasDatabaseName("IX_Badge_Name");
    }
}