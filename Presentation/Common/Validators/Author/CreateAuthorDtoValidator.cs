using Application.Common.Dtos.Author;
using FluentValidation;

namespace Presentation.Common.Validators.Author;

public class CreateAuthorDtoValidator : AbstractValidator<CreateAuthorDto>
{
    public CreateAuthorDtoValidator()
    {
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("The last name is required.")
            .LastNameRule();

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("The first name is required")
            .FirstNameRule();

        RuleFor(x => x.MiddleName)!
            .MiddleNameRule();

        RuleFor(x => x.Description)!
            .DescriptionRule();
    }
}