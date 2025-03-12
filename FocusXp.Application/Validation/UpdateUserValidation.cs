using System;
using FluentValidation;
using FocusXp.Application.Command;
using FocusXp.Domain.Constant;

namespace FocusXp.Application.Validation;

public class UpdateUserValidation : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidation()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage(ErrorMessages.Required.UserId);

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage(ErrorMessages.Required.Username)
            .MaximumLength(50).WithMessage(ErrorMessages.StringLength.Username);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ErrorMessages.Required.Email)
            .EmailAddress().WithMessage(ErrorMessages.InvalidFormat.Email);

        RuleFor(x => x.Fullname)
            .NotEmpty().WithMessage(ErrorMessages.Required.Fullname)
            .MaximumLength(50).WithMessage(ErrorMessages.StringLength.Fullname);
    }
}
