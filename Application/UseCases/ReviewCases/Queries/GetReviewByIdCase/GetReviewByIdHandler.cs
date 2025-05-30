using Application.Common.Dtos.Review;
using Application.Common.Utils;
using AutoMapper;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.ReviewCases.Queries.GetReviewByIdCase;

public class GetReviewByIdHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetReviewByIdQuery, Result<ReadReviewDto>>
{
    public async Task<Result<ReadReviewDto>> Handle(GetReviewByIdQuery getReviewByIdQuery, CancellationToken cancellationToken)
    {
        var review = await unitOfWork.Reviews.GetByIdAsync(getReviewByIdQuery.Id, cancellationToken);
        if (review is null)
        {
            return ResultBuilder.NotFoundResult<ReadReviewDto>(ErrorMessages.ReviewIdNotFound);
        }

        var reviewReadDto = mapper.Map<ReadReviewDto>(review);

        return ResultBuilder.SuccessResult(reviewReadDto);
    }
}