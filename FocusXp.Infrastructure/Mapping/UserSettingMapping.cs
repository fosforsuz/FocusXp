using FocusXp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusXp.Infrastructure.Mapping;

public class UserSettingMapping : IEntityTypeConfiguration<UserSetting>
{
    public void Configure(EntityTypeBuilder<UserSetting> builder)
    {
        builder.Property(setting => setting.Id)
            .ValueGeneratedNever();

        builder.HasOne(setting => setting.User)
            .WithOne(user => user.UserSetting)
            .HasForeignKey<UserSetting>(setting => setting.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(setting => setting.UserId)
            .IsUnique()
            .HasDatabaseName("IX_user_settings_user_id");

        builder.Property(setting => setting.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnUpdate();
    }
}