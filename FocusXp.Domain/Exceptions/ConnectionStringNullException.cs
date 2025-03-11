using FocusXp.Domain.Constant;

namespace FocusXp.Domain.Exceptions;

public class ConnectionStringNullException : Exception
{
    public ConnectionStringNullException() : base(ExceptionMessages.ConnectionStringNotFound)
    {
    }

    public ConnectionStringNullException(string name) : base(
        string.Format(ExceptionMessages.ConnectionStringWithNamedNotFound, name))
    {
    }

    public ConnectionStringNullException(string message, Exception innerException) : base(message, innerException)
    {
    }
}