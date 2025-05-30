using Application.Common.Utils;
using AutoMapper;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.ReviewCases.Commands.DeleteReviewCase;

public class DeleteReviewHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteReviewCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteReviewCommand deleteReviewCommand, CancellationToken cancellationToken)
    {
        var review = await unitOfWork.Reviews.GetByIdAsync(deleteReviewCommand.Id, cancellationToken);
        if (review is null)
        {
            return ResultBuilder.NotFoundResult<Unit>(ErrorMessages.ReviewIdNotFound);
        }

        await unitOfWork.Reviews.Delete(review);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultBuilder.NoContentResult<Unit>();
    }
}