using FluentValidation;
using Models.Core;

namespace Validation.Validators;

public class MediaValidator : AbstractValidator<MediaDto>
{
	public MediaValidator()
	{
		RuleFor(x => x.Image).NotNull().WithMessage("Please, choose the image");
	}
}
