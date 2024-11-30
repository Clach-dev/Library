using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.AuthorCases.Commands.DeleteAuthorCase;

public class DeleteAuthorHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
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