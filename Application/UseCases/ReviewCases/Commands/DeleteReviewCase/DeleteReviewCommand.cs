using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.ReviewCases.Commands.DeleteReviewCase;

public record DeleteReviewCommand(
    Guid Id)
    : IRequest<Result<Unit>>;