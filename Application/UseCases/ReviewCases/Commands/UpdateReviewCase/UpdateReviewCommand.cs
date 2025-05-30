using Application.Common.Dtos.Review;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.ReviewCases.Commands.UpdateReviewCase;

public record UpdateReviewCommand(
    Guid Id,
    decimal? Rating,
    string? Comment)
    : IRequest<Result<ReadReviewDto>>;
