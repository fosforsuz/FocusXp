using FluentResults;
using FocusXp.Application.Interface;
using FocusXp.Infrastructure.Interfaces.Data;

namespace FocusXp.Application.Service;

public class BaseService : IBaseService
{
    private readonly IUnitOfWork _unitOfWork;

    protected BaseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<T>> ExecuteInTransactionAsync<T>(Func<Task<Result<T>>> action,
        CancellationToken cancellationToken)
    {
        var isNewTransaction = !_unitOfWork.IsTransactionExists();

        if (isNewTransaction)
            await _unitOfWork.BeginTransactionAsync(cancellationToken: cancellationToken);

        try
        {
            var result = await action();

            if (result.IsSuccess)
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (isNewTransaction)
                    await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            else if (isNewTransaction)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            }

            return result;
        }
        catch (Exception ex)
        {
            if (isNewTransaction)
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            var errorMessage = $"❌ Transaction failed: {ex.Message} \n StackTrace: {ex.StackTrace}";
            return Result.Fail<T>(errorMessage);
        }
    }
}