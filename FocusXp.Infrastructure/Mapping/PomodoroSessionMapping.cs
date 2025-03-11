using FocusXp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusXp.Infrastructure.Mapping;

public class PomodoroSessionMapping : IEntityTypeConfiguration<PomodoroSession>
{
    public void Configure(EntityTypeBuilder<PomodoroSession> builder)
    {
        builder.Property(session => session.Id)
            .ValueGeneratedNever();

        builder.HasOne(session => session.WorkItem)
            .WithMany(item => item.PomodoroSessions)
            .HasForeignKey(session => session.WorkItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(session => session.User)
            .WithMany(user => user.PomodoroSessions)
            .HasForeignKey(session => session.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ps => ps.UserId)
            .HasDatabaseName("IX_PomodoroSessions_UserId");

        builder.HasIndex(ps => ps.WorkItemId)
            .HasDatabaseName("IX_PomodoroSessions_WorkItemId");
    }
}