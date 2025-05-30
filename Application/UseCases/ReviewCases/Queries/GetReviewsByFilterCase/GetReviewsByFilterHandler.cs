using Application.Common.Dtos.Review;
using Application.Common.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.ReviewCases.Queries.GetReviewsByFilterCase;

public class GetReviewsByFilterHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) 
    : IRequestHandler<GetReviewsByFilterQuery, Result<ReadReviewsDto>>
{
    public async Task<Result<ReadReviewsDto>> Handle(
        GetReviewsByFilterQuery getReviewsByFilterQuery, 
        CancellationToken cancellationToken)
    {
        var reviews = await unitOfWork.Reviews.GetByPredicateAsync(review =>
                (getReviewsByFilterQuery.UserId == null ||
                 review.UserId == getReviewsByFilterQuery.UserId) && 
                (getReviewsByFilterQuery.BookId == null ||
                 review.BookId == getReviewsByFilterQuery.BookId) &&
                (getReviewsByFilterQuery.WithComment == null ||
                 (getReviewsByFilterQuery.WithComment == true && review.Comment != null) ||
                    (getReviewsByFilterQuery.WithComment == false && review.Comment == null)),
            mapper.Map<PageInfo>(getReviewsByFilterQuery.PageInfoDto),
            cancellationToken);
        
        var reviewsReadDto = mapper.Map<ReadReviewsDto>(reviews);

        return ResultBuilder.SuccessResult(reviewsReadDto);
    }
}