using Application.Dtos.Author;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.AuthorCases.Commands.UpdateAuthorCase;

public class AuthorUpdateHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateAuthorCommand, Result<AuthorReadDto>>
{
    public async Task<Result<AuthorReadDto>> Handle(
        UpdateAuthorCommand updateAuthorCommand,
        CancellationToken cancellationToken)
    {
        var currentAuthor = await unitOfWork.Authors.GetByIdAsync(updateAuthorCommand.Id, cancellationToken);

        if (currentAuthor is null)
        {
            return ResultBuilder.NotFoundResult<AuthorReadDto>(ErrorMessages.NotFoundError);
        }

        mapper.Map(updateAuthorCommand, currentAuthor);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var authorReadDto = mapper.Map<AuthorReadDto>(currentAuthor);
        return ResultBuilder.SuccessResult(authorReadDto);
    }
}