using FocusXp.Domain.Entities;
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
}