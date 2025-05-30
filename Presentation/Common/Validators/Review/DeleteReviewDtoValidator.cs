using Application.Common.Dtos.Review;
using FluentValidation;

namespace Presentation.Common.Validators.Review;

public class DeleteReviewDtoValidator : AbstractValidator<DeleteReviewDto>
{
    public DeleteReviewDtoValidator()
    {
        RuleFor(x => x.Id)
            .GuidRule();
    }
}