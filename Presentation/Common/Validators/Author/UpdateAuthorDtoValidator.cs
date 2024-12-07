using Application.Common.Dtos.Author;
using FluentValidation;

namespace Presentation.Common.Validators.Author;

public class UpdateAuthorDtoValidator : AbstractValidator<UpdateAuthorDto>
{
    public UpdateAuthorDtoValidator()
    {
        RuleFor(x => x.Id)
            .GuidRule();
        
        RuleFor(x => x.LastName)!
            .LastNameRule();

        RuleFor(x => x.FirstName)!
            .FirstNameRule();

        RuleFor(x => x.MiddleName)!
            .MiddleNameRule();

        RuleFor(x => x.Description)!
            .DescriptionRule();
    }
}