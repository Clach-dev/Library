using Application.Common.Dtos.Author;
using FluentValidation;

namespace Presentation.Common.Validators.Author;

public class AuthorsByNameDtoValidator : AbstractValidator<AuthorsByNameDto>
{
    public AuthorsByNameDtoValidator()
    {
        RuleFor(x => x.PageInfo)
            .SetValidator(new PageInfoValidator());
        
        RuleFor(x => x.LastName)!
            .LastNameRule();

        RuleFor(x => x.FirstName)!
            .FirstNameRule();
    }
}