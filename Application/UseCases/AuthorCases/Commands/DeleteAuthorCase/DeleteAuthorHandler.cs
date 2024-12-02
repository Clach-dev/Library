using Application.Common.Interfaces.IRepositories;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.AuthorCases.Commands.DeleteAuthorCase;

public class DeleteAuthorHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteAuthorCommand, Result<byte>>
{
    public async Task<Result<byte>> Handle(DeleteAuthorCommand deleteAuthorCommand, CancellationToken cancellationToken)
    {
        var author = await unitOfWork.Authors.GetByIdAsync(deleteAuthorCommand.Id, cancellationToken);
        if (author is null)
        {
            return ResultBuilder.NotFoundResult<byte>(ErrorMessages.AuthorIdNotFound);
        }
        
        await unitOfWork.Authors.Delete(author);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultBuilder.NoContentResult<byte>();
    }
}