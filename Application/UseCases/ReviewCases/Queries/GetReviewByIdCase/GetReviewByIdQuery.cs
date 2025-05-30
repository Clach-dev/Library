using Application.Common.Dtos.Review;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.ReviewCases.Queries.GetReviewByIdCase;

public record GetReviewByIdQuery(
    Guid Id)
    : IRequest<Result<ReadReviewDto>>;