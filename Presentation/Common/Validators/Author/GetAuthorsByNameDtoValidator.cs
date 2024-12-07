using Application.Common.Dtos.Author;
using FluentValidation;

namespace Presentation.Common.Validators.Author;

public class GetAuthorsByNameDtoValidator : AbstractValidator<GetAuthorsByNameDto>
{
    public GetAuthorsByNameDtoValidator()
    {
        RuleFor(x => x.PageInfo)
            .SetValidator(new PageInfoValidator());
        
        RuleFor(x => x.LastName)!
            .LastNameRule();

        RuleFor(x => x.FirstName)!
            .FirstNameRule();
    }
}