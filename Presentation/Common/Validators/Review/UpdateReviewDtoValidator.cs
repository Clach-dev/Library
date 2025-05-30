using Application.Common.Dtos.Review;
using FluentValidation;

namespace Presentation.Common.Validators.Review;

public class UpdateReviewDtoValidator : AbstractValidator<UpdateReviewDto>
{
    public UpdateReviewDtoValidator()
    {
        RuleFor(x => x.Id)
            .GuidRule();

        RuleFor(x => x.Rating)
            .RatingRule();

        RuleFor(x => x.Comment)
            .DescriptionRule();
    }
}