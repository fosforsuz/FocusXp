using FocusXp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusXp.Infrastructure.Mapping;

public class UserBadgeMapping : IEntityTypeConfiguration<UserBadge>
{
    public void Configure(EntityTypeBuilder<UserBadge> builder)
    {
        builder.Property(badge => badge.Id)
            .ValueGeneratedNever();

        builder.HasOne(badge => badge.User)
            .WithMany(user => user.UserBadges)
            .HasForeignKey(badge => badge.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(badge => badge.UserId)
            .HasDatabaseName("IX_user_badges_user_id");
        
        builder.HasOne(badge => badge.Badge)
            .WithMany(badge => badge.UserBadges)
            .HasForeignKey(badge => badge.BadgeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(badge => badge.BadgeId)
            .HasDatabaseName("IX_user_badges_badge_id");
    }
}