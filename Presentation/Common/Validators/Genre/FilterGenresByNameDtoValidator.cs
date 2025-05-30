using Application.Common.Dtos.Genre;
using FluentValidation;

namespace Presentation.Common.Validators.Genre;

public class FilterGenresByNameDtoValidator : AbstractValidator<FilterGenresByNameDto>
{
    public FilterGenresByNameDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNullRule()
            .TitleRule();
        
        RuleFor(x => x.PageInfoDto)
            .SetValidator(new PageInfoValidator());
    }
}