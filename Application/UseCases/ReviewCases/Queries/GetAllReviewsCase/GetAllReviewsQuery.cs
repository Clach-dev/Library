using Application.Common.Dtos;
using Application.Common.Dtos.Review;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.ReviewCases.Queries.GetAllReviewsCase;

public record GetAllReviewsQuery(
    PageInfoDto PageInfoDto)
    : IRequest<Result<ReadReviewsDto>>;