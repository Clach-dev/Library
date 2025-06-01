using Application.Common.Dtos.Review;
using Application.Common.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.ReviewCases.Queries.GetAllReviewsCase;

public class GetAllReviewsHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) 
    : IRequestHandler<GetAllReviewsQuery, Result<ReadReviewsDto>>
{
    public async Task<Result<ReadReviewsDto>> Handle(
        GetAllReviewsQuery getAllReviewsQuery,
        CancellationToken cancellationToken)
    {
        var reviews = await unitOfWork.Reviews.GetAllAsync(
            mapper.Map<PageInfo>(getAllReviewsQuery.PageInfoDto),
            cancellationToken);

        var reviewsReadDto = new ReadReviewsDto(mapper.Map<IEnumerable<ReadReviewDto>>(reviews.Item1), reviews.Item2);

        return ResultBuilder.SuccessResult(reviewsReadDto);
    }
}