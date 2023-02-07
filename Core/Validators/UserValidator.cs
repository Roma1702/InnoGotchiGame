using FluentValidation;
using Models.Core;

namespace Validation.Validators;

public class UserValidator : AbstractValidator<UserDto>
{
	public UserValidator()
	{
		RuleFor(x => x.Name).NotEmpty().WithMessage("User name is required");
        RuleFor(x => x.Surname).NotEmpty().WithMessage("User surname is required");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email address is required")
            .EmailAddress().WithMessage("A valid email is required");
		RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage($"Minimum lenght of password should be 6 characters");
        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirm password is required")
            .Equal(x => x.Password).WithMessage("Passwords are not equals");
    }
}
