using FluentValidation;
using Models.Core;

namespace Validation.Validators;

public class ShortUserValidator : AbstractValidator<ShortUserDto>
{
	public ShortUserValidator()
	{
        RuleFor(x => x.Name).NotEmpty().WithMessage("User name is required");
        RuleFor(x => x.Surname).NotEmpty().WithMessage("User surname is required");
    }
}
