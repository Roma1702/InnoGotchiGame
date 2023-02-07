using FluentValidation;
using Models.Core;

namespace Validation.Validators;

public class InnogotchiStateValidator : AbstractValidator<InnogotchiStateDto>
{
	public InnogotchiStateValidator()
	{
		RuleFor(x => x.Hunger).IsInEnum().WithMessage("Your pet is fully fed");
		RuleFor(x => x.Thirsty).IsInEnum().WithMessage("Your pet is fully drunk");
	}
}
