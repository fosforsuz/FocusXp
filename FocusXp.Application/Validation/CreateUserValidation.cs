using FluentValidation;
using FocusXp.Application.Command;
using FocusXp.Domain.Constant;

namespace FocusXp.Application.Validation;

public class CreateUserValidation : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidation()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage(ErrorMessages.Required.Username)
            .Length(3, 100).WithMessage(ErrorMessages.StringLength.Username);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ErrorMessages.Required.Email)
            .EmailAddress().WithMessage(ErrorMessages.InvalidFormat.Email)
            .MaximumLength(100).WithMessage(ErrorMessages.StringLength.Email);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(ErrorMessages.Required.Password)
            .MinimumLength(8).WithMessage(ErrorMessages.StringLength.PasswordHash);

        RuleFor(x => x.Fullname)
            .NotEmpty().WithMessage(ErrorMessages.Required.Fullname)
            .Length(3, 100).WithMessage(ErrorMessages.StringLength.Fullname);

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage(ErrorMessages.Required.ConfirmPassword)
            .Equal(x => x.Password).WithMessage(ErrorMessages.InvalidFormat.PasswordNotMatch);
    }
}