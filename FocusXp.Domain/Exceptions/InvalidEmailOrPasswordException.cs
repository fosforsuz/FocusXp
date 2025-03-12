using FocusXp.Domain.Constant;

namespace FocusXp.Domain.Exceptions;

public class InvalidEmailOrPasswordException : Exception
{
    public InvalidEmailOrPasswordException() : base(ErrorMessages.Authentication.InvalidLogin)
    {
    }
}