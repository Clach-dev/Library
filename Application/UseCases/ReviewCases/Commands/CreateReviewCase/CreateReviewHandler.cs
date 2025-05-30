using Application.Common.Dtos.Review;
using Application.Common.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.ReviewCases.Commands.CreateReviewCase;

public class CreateReviewHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateReviewCommand, Result<ReadReviewDto>>
{
    public async Task<Result<ReadReviewDto>> Handle(
        CreateReviewCommand createReviewCommand,
        CancellationToken cancellationToken)
    {
        var book = await unitOfWork.Books.GetByIdAsync(createReviewCommand.BookId, cancellationToken);
        if (book is null)
        {
            return ResultBuilder.NotFoundResult<ReadReviewDto>(ErrorMessages.BookIdNotFound);
        }

        var user = await unitOfWork.Users.GetByIdAsync(createReviewCommand.UserId, cancellationToken);
        if (user is null)
        {
            return ResultBuilder.NotFoundResult<ReadReviewDto>(ErrorMessages.UserIdNotFound);
        }

        var existedReview = (await unitOfWork.Reviews.GetByPredicateAsync(review =>
                        review.UserId == createReviewCommand.UserId &&
                        review.BookId == createReviewCommand.BookId &&
                        review.Rating == createReviewCommand.Rating,
                    new PageInfo(),
                    cancellationToken))
            .Item1.FirstOrDefault();
        if (existedReview is not null)
        {
            return ResultBuilder.ConflictResult<ReadReviewDto>(ErrorMessages.ExistingReviewError);
        }
        
        var newReview = mapper.Map<Review>(createReviewCommand);
        
        await unitOfWork.Reviews.CreateAsync(newReview, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var reviewReadDto = mapper.Map<ReadReviewDto>(newReview);
        return ResultBuilder.CreatedResult(reviewReadDto);
    }
}