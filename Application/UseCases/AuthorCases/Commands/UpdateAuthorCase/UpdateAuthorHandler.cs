using Application.Dtos.Author;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.AuthorCases.Commands.UpdateAuthorCase;

public class UpdateAuthorHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateAuthorCommand, Result<ReadAuthorDto>>
{
    public async Task<Result<ReadAuthorDto>> Handle(
        UpdateAuthorCommand updateAuthorCommand,
        CancellationToken cancellationToken)
    {
        var currentAuthor = await unitOfWork.Authors.GetByIdAsync(updateAuthorCommand.Id, cancellationToken);
        if (currentAuthor is null)
        {
            return ResultBuilder.NotFoundResult<ReadAuthorDto>(ErrorMessages.NotFoundError);
        }

        mapper.Map(updateAuthorCommand, currentAuthor);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var authorReadDto = mapper.Map<ReadAuthorDto>(currentAuthor);
        return ResultBuilder.SuccessResult(authorReadDto);
    }
}