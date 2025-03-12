using FluentValidation;
using FocusXp.Application.Command;
using FocusXp.Domain.Constant;

namespace FocusXp.Application.Validation;

public class UpdatePasswordValidation : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordValidation()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage(ErrorMessages.Required.UserId);

        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage(ErrorMessages.Required.OldPassword);

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage(ErrorMessages.Required.NewPassword)
            .MinimumLength(6).WithMessage(ErrorMessages.StringLength.MinSixLength);

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage(ErrorMessages.Required.ConfirmPassword)
            .Equal(x => x.NewPassword).WithMessage(ErrorMessages.InvalidFormat.PasswordNotMatch);
    }
}