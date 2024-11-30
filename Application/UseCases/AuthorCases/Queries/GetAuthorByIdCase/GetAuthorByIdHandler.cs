using Application.Dtos.Author;
using Application.Dtos.User;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.AuthorCases.Queries.GetAuthorByIdCase;

public class GetAuthorByIdHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetAuthorByIdQuery, Result<AuthorReadDto>>
{
    public async Task<Result<AuthorReadDto>> Handle(GetAuthorByIdQuery getAuthorByIdQuery, CancellationToken cancellationToken)
    {
        var author = await unitOfWork.Authors.GetByIdAsync(getAuthorByIdQuery.Id, cancellationToken);

        if (author is null)
        {
            ResultBuilder.NotFoundResult<AuthorReadDto>(ErrorMessages.AuthorIdNotFound);
        }
        
        var authorReadDto = mapper.Map<AuthorReadDto>(author);

        return ResultBuilder.SuccessResult(authorReadDto);
    }
}