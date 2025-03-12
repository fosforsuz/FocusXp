using FluentValidation;
using FocusXp.Application.Command;
using FocusXp.Domain.Constant;

namespace FocusXp.Application.Validation;

public class LoginValidation : AbstractValidator<LoginCommand>
{
    public LoginValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ErrorMessages.Required.Email)
            .EmailAddress().WithMessage(ErrorMessages.InvalidFormat.Email);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(ErrorMessages.Required.Password);
    }
}