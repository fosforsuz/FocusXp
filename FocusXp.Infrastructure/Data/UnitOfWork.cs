using System.Collections.Concurrent;
using System.Data;
using FocusXp.Domain.Exceptions;
using FocusXp.Domain.Interfaces;
using FocusXp.Infrastructure.Interfaces.Data;
using FocusXp.Infrastructure.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace FocusXp.Infrastructure.Data;

internal class UnitOfWork : IUnitOfWork
{
    #region Properties and Fields

    private readonly DbContext _context;
    private readonly ConcurrentDictionary<Type, object> _customRepositories = new();
    private readonly ConcurrentDictionary<Type, object> _repositories = new();
    private readonly IServiceProvider _serviceProvider;
    private bool _disposed;
    private IDbContextTransaction? _transaction;

    #endregion

    public UnitOfWork(FocusXpContext context, IServiceProvider serviceProvider)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    #region Repositories

    public IAttachmentRepository Attachments => GetCustomRepository<IAttachmentRepository>();
    public IBadgeRepository Badges => GetCustomRepository<IBadgeRepository>();
    public IPomodoroDistractionRepository PomodoroDistractions => GetCustomRepository<IPomodoroDistractionRepository>();
    public IPomodoroSessionRepository PomodoroSessions => GetCustomRepository<IPomodoroSessionRepository>();
    public ISubWorkItemRepository SubWorkItems => GetCustomRepository<ISubWorkItemRepository>();
    public ITagRepository Tags => GetCustomRepository<ITagRepository>();
    public IUserRepository Users => GetCustomRepository<IUserRepository>();
    public IUserBadgeRepository UserBadges => GetCustomRepository<IUserBadgeRepository>();
    public IUserDailyStatisticRepository UserDailyStatistics => GetCustomRepository<IUserDailyStatisticRepository>();
    public IUserSettingRepository UserSettings => GetCustomRepository<IUserSettingRepository>();
    public IWorkItemRepository WorkItems => GetCustomRepository<IWorkItemRepository>();
    public IWorkItemTagRepository WorkItemTags => GetCustomRepository<IWorkItemTagRepository>();
    public IXpTransactionRepository XpTransactions => GetCustomRepository<IXpTransactionRepository>();

    #endregion

    #region Repository Methods

    public IRepository<T> GetRepository<T>() where T : class, IBaseEntity
    {
        return (IRepository<T>)_repositories.GetOrAdd(typeof(T),
            _ => _serviceProvider.GetRequiredService<IRepository<T>>());
    }

    public TRepository GetCustomRepository<TRepository>() where TRepository : class
    {
        return (TRepository)_customRepositories.GetOrAdd(typeof(TRepository),
            _ => _serviceProvider.GetRequiredService<TRepository>());
    }

    #endregion

    #region Transaction Management

    public async Task BeginTransactionAsync(bool useDistributedTransaction = false,
        CancellationToken cancellationToken = default)
    {
        if (_transaction is not null)
            throw new TransactionAlreadyStartedException();


        _transaction = useDistributedTransaction
            ? await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken)
            : await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null)
            throw new TransactionNotStartedException();

        try
        {
            await _transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null)
            throw new TransactionNotStartedException();

        await _transaction.RollbackAsync(cancellationToken);
        await DisposeTransactionAsync();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(UnitOfWork),
                "This UnitOfWork instance has already been disposed.");

        var affectedRows = await _context.SaveChangesAsync(cancellationToken);

        if (_transaction != null)
            await CommitTransactionAsync(cancellationToken);


        return affectedRows;
    }

    private async Task DisposeTransactionAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    #endregion

    #region Dispose

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            _context.Dispose();
            _transaction?.Dispose();
        }

        _disposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;

        _disposed = true;
        await _context.DisposeAsync();
        if (_transaction != null) await _transaction.DisposeAsync();

        GC.SuppressFinalize(this);
    }

    #endregion
}