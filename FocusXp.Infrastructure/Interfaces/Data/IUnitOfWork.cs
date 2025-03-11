using FocusXp.Domain.Interfaces;
using FocusXp.Infrastructure.Interfaces.Repository;

namespace FocusXp.Infrastructure.Interfaces.Data;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    IAttachmentRepository Attachments { get; }
    IBadgeRepository Badges { get; }
    IPomodoroDistractionRepository PomodoroDistractions { get; }
    IPomodoroSessionRepository PomodoroSessions { get; }
    ISubWorkItemRepository SubWorkItems { get; }
    ITagRepository Tags { get; }
    IUserRepository Users { get; }
    IUserBadgeRepository UserBadges { get; }
    IUserDailyStatisticRepository UserDailyStatistics { get; }
    IUserSettingRepository UserSettings { get; }
    IWorkItemRepository WorkItems { get; }
    IWorkItemTagRepository WorkItemTags { get; }
    IXpTransactionRepository XpTransactions { get; }

    IRepository<T> GetRepository<T>() where T : class, IBaseEntity;
    TRepository GetCustomRepository<TRepository>() where TRepository : class;
    Task BeginTransactionAsync(bool useDistributedTransaction = false, CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}