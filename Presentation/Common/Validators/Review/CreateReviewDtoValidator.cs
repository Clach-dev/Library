using Application.Common.Dtos.Review;
using FluentValidation;

namespace Presentation.Common.Validators.Review;

public class CreateReviewDtoValidator : AbstractValidator<CreateReviewDto>
{
    public CreateReviewDtoValidator()
    {
        RuleFor(x => x.BookId).
            GuidRule();

        RuleFor(x => x.Rating)
            .RatingRule();
        
        RuleFor(x => x.Comment)
            .DescriptionRule();
    }
}