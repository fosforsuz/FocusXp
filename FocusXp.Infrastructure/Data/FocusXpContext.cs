using FocusXp.Domain.Entities;
using FocusXp.Domain.Enum;
using FocusXp.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace FocusXp.Infrastructure.Data;

public class FocusXpContext : DbContext
{
    public FocusXpContext()
    {
    }

    public FocusXpContext(DbContextOptions<FocusXpContext> options) : base(options)
    {
    }

    public virtual DbSet<Attachment> Attachments { get; set; }
    public virtual DbSet<Badge> Badges { get; set; }
    public virtual DbSet<PomodoroDistraction> PomodoroDistractions { get; set; }
    public virtual DbSet<PomodoroSession> PomodoroSessions { get; set; }
    public virtual DbSet<SubWorkItem> SubWorkItems { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserBadge> UserBadges { get; set; }
    public virtual DbSet<UserDailyStatistic> UsersDailyStatistics { get; set; }
    public virtual DbSet<UserSetting> UserSettings { get; set; }
    public virtual DbSet<WorkItem> WorkItems { get; set; }
    public virtual DbSet<WorkItemTag> WorkItemTags { get; set; }
    public virtual DbSet<XpTransaction> XpTransactions { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=.;Database=FocusXp;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureEnums(modelBuilder);
        modelBuilder.ApplyConfiguration(new AttachmentMapping());
        modelBuilder.ApplyConfiguration(new BadgeMapping());
        modelBuilder.ApplyConfiguration(new PomodoroDistractionMapping());
        modelBuilder.ApplyConfiguration(new PomodoroSessionMapping());
        modelBuilder.ApplyConfiguration(new SubWorkItemMapping());
        modelBuilder.ApplyConfiguration(new TagMapping());
        modelBuilder.ApplyConfiguration(new UserBadgeMapping());
        modelBuilder.ApplyConfiguration(new UserDailyStatisticMapping());
        modelBuilder.ApplyConfiguration(new UserSettingMapping());
        modelBuilder.ApplyConfiguration(new UserMapping());
        modelBuilder.ApplyConfiguration(new WorkItemMapping());
        modelBuilder.ApplyConfiguration(new WorkItemTagMapping());
        modelBuilder.ApplyConfiguration(new XpTransactionMapping());
    }

    private static void ConfigureEnums(ModelBuilder builder)
    {
        builder.HasPostgresEnum<DistractionType>();
        builder.HasPostgresEnum<FocusLevel>();
        builder.HasPostgresEnum<Priority>();
        builder.HasPostgresEnum<RecurrenceType>();
        builder.HasPostgresEnum<Role>();
        builder.HasPostgresEnum<SessionType>();
        builder.HasPostgresEnum<Theme>();
        builder.HasPostgresEnum<WorkItemStatus>();
        builder.HasPostgresEnum<XpTransactionType>();
    }
}