using Application.Common.Dtos.Author;
using FluentValidation;

namespace Presentation.Common.Validators.Author;

public class CreateAuthorDtoValidator : AbstractValidator<CreateAuthorDto>
{
    public CreateAuthorDtoValidator()
    {
        RuleFor(x => x.LastName)
            .NotNullRule()
            .LastNameRule();

        RuleFor(x => x.FirstName)
            .NotNullRule()
            .FirstNameRule();

        RuleFor(x => x.MiddleName)
            .MiddleNameRule();
        
        RuleFor(x => x.Description)
            .DescriptionRule();
    }
}