using Application.Common.Dtos.Author;
using FluentValidation;

namespace Presentation.Common.Validators.Author;

public class FilterAuthorsByNameDtoValidator : AbstractValidator<FilterAuthorsByNameDto>
{
    public FilterAuthorsByNameDtoValidator()
    {
        RuleFor(x => x.LastName)
            .LastNameRule();

        RuleFor(x => x.FirstName)
            .FirstNameRule();
        
        RuleFor(x => x.PageInfoDto)
            .SetValidator(new PageInfoValidator());
    }
}