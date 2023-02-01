using FluentValidation;
using Models.Core;

namespace Validation.Validators;

public class FarmValidator : AbstractValidator<FarmDto>
{
	public FarmValidator()
	{
		RuleFor(x => x.Name).NotEmpty().WithMessage("Farm name is required");
	}
}
