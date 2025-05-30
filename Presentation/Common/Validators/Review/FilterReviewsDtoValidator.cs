using Application.Common.Dtos.Review;
using FluentValidation;

namespace Presentation.Common.Validators.Review;

public class FilterReviewsDtoValidator : AbstractValidator<FilterReviewsDto>
{
    public FilterReviewsDtoValidator()
    {
        RuleFor(x => x.BookId)
            .GuidRule();

        RuleFor(x => x.UserId)
            .GuidRule();

        RuleFor(x => x.WithComment)
            .WithCommentRule();

        RuleFor(x => x.PageInfoDto)
            .SetValidator(new PageInfoValidator());
    }
}