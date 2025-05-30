using Application.Common.Dtos;
using Application.Common.Dtos.Review;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.ReviewCases.Queries.GetReviewsByFilterCase;

public record GetReviewsByFilterQuery(
    Guid? BookId,
    Guid? UserId,
    bool? WithComment,
    PageInfoDto PageInfoDto)
    : IRequest<Result<ReadReviewsDto>>;