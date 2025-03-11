using FocusXp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusXp.Infrastructure.Mapping;

public class PomodoroDistractionMapping : IEntityTypeConfiguration<PomodoroDistraction>
{
    public void Configure(EntityTypeBuilder<PomodoroDistraction> builder)
    {
        builder.Property(distraction => distraction.Id)
            .ValueGeneratedNever();

        builder.HasOne(distraction => distraction.PomodoroSession)
            .WithMany(session => session.Distractions)
            .HasForeignKey(distraction => distraction.PomodoroSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(distraction => distraction.PomodoroSessionId)
            .HasDatabaseName("IX_PomodoroSession_Distractions");
    }
}