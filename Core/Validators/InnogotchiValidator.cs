using FluentValidation;
using Models.Core;

namespace Validation.Validators;

public class InnogotchiValidator : AbstractValidator<InnogotchiDto>
{
	public InnogotchiValidator()
	{
		RuleFor(x => x.Name).NotEmpty().WithMessage("Your pet name is required");
	}
}
