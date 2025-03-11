using FocusXp.Domain.Constant;

namespace FocusXp.Domain.Exceptions;

public class TransactionAlreadyStartedException : Exception
{
    public TransactionAlreadyStartedException() : base(ExceptionMessages.TransactionAlreadyStarted)
    {
    }

    public TransactionAlreadyStartedException(string message) : base(message)
    {
    }
}