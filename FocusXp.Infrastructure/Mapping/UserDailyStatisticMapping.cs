using FocusXp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusXp.Infrastructure.Mapping;

public class UserDailyStatisticMapping : IEntityTypeConfiguration<UserDailyStatistic>
{
    public void Configure(EntityTypeBuilder<UserDailyStatistic> builder)
    {
        builder.Property(statistic => statistic.Id)
            .ValueGeneratedNever();

        builder.HasOne(statistic => statistic.User)
            .WithMany(user => user.UserDailyStatistics)
            .HasForeignKey(statistic => statistic.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(statistic => statistic.UserId)
            .HasDatabaseName("user_daily_statistics_user_id_index");
        
        builder.HasIndex(statistic => statistic.Date)
            .HasDatabaseName("user_daily_statistics_date_index");
        
        builder.HasIndex(statistic => new {statistic.UserId, statistic.Date})
            .IsUnique()
            .HasDatabaseName("user_daily_statistics_user_id_date_index");
    }
}