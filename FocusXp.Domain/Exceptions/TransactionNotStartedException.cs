using FocusXp.Domain.Constant;

namespace FocusXp.Domain.Exceptions;

public class TransactionNotStartedException : Exception
{
    public TransactionNotStartedException() : base(ExceptionMessages.TransactionNotStarted)
    {
    }

    public TransactionNotStartedException(string message) : base(message)
    {
    }
}