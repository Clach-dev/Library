using Application.Common.Dtos.Book;
using FluentValidation;

namespace Presentation.Common.Validators.Book;

public class FilterBooksDtoValidator : AbstractValidator<FilterBooksDto>
{
    public FilterBooksDtoValidator()
    {
        RuleFor(x => x.Title)
            .TitleRule();

        RuleFor(x => x.LowerAgeLimit)
            .AgeLimitRule();
        
        RuleFor(x => x.UpperAgeLimit)
            .AgeLimitRule();
        
        RuleFor(x => x.AuthorsIds)
            .GuidListRule();
        
        RuleFor(x => x.GenresIds)
            .GuidListRule();
        
        RuleFor(x => x.PageInfoDto)
            .SetValidator(new PageInfoValidator());
    }
}