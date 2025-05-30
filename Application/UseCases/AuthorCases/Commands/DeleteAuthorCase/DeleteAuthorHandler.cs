using Application.Common.Utils;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.AuthorCases.Commands.DeleteAuthorCase;

public class DeleteAuthorHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteAuthorCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        DeleteAuthorCommand deleteAuthorCommand,
        CancellationToken cancellationToken)
    {
        var author = await unitOfWork.Authors.GetByIdAsync(deleteAuthorCommand.Id, cancellationToken);
        if (author is null)
        {
            return ResultBuilder.NotFoundResult<Unit>(ErrorMessages.AuthorIdNotFound);
        }
        
        await unitOfWork.Authors.Delete(author);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultBuilder.NoContentResult<Unit>();
    }
}