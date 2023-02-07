using FluentValidation;
using Models.Core;

namespace Validation.Validators;

public class ChangeUserPasswordValidator : AbstractValidator<ChangePasswordDto>
{
	public ChangeUserPasswordValidator()
	{
		RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("This filed is required")
            .MinimumLength(6).WithMessage($"Minimum lenght of password should be 6 characters");
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage("This filed is required")
            .MinimumLength(6).WithMessage($"Minimum lenght of password should be 6 characters");
        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("This filed is required")
            .MinimumLength(6).WithMessage($"Minimum lenght of password should be 6 characters")
            .Equal(x => x.NewPassword).WithMessage("Passwords are not equals");
    }
}
