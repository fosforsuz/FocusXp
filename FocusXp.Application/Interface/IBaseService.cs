using FluentResults;

namespace FocusXp.Application.Interface;

public interface IBaseService
{
    Task<Result<T>> ExecuteInTransactionAsync<T>(Func<Task<Result<T>>> action, CancellationToken cancellationToken);
}