using Application.Common.Interfaces.IRepositories;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.GenreCases.Commands.DeleteGenreCase;

public class DeleteGenreHandler(
    IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteGenreCommand, Result<byte>>
{
    public async Task<Result<byte>> Handle(
        DeleteGenreCommand deleteGenreCommand,
        CancellationToken cancellationToken)
    {
        var genre = await unitOfWork.Genres.GetByIdAsync(deleteGenreCommand.Id, cancellationToken);
        if (genre is null)
        {
            return ResultBuilder.NotFoundResult<byte>(ErrorMessages.NotFoundError);
        }
        
        await unitOfWork.Genres.Delete(genre);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return ResultBuilder.NoContentResult<byte>();
    }
}