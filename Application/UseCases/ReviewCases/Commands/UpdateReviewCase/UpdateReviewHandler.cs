using Application.Common.Dtos.Review;
using Application.Common.Utils;
using AutoMapper;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.ReviewCases.Commands.UpdateReviewCase;

public class UpdateReviewHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateReviewCommand, Result<ReadReviewDto>>
{
    public async Task<Result<ReadReviewDto>> Handle(UpdateReviewCommand updateReviewCommand, CancellationToken cancellationToken)
    {
        var currentReview = await unitOfWork.Reviews.GetByIdAsync(updateReviewCommand.Id, cancellationToken);
        if (currentReview is null)
        {
            return ResultBuilder.NotFoundResult<ReadReviewDto>(ErrorMessages.ReviewIdNotFound);
        }
        
        mapper.Map(updateReviewCommand, currentReview);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var reviewReadDto = mapper.Map<ReadReviewDto>(currentReview);
        return ResultBuilder.SuccessResult(reviewReadDto);
    }
}