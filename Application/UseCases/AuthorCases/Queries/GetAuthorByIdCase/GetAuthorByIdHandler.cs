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
    : IRequestHandler<GetAuthorByIdQuery, Result<ReadAuthorDto>>
{
    public async Task<Result<ReadAuthorDto>> Handle(GetAuthorByIdQuery getAuthorByIdQuery, CancellationToken cancellationToken)
    {
        var author = await unitOfWork.Authors.GetByIdAsync(getAuthorByIdQuery.Id, cancellationToken);

        if (author is null)
        {
            ResultBuilder.NotFoundResult<ReadAuthorDto>(ErrorMessages.AuthorIdNotFound);
        }
        
        var authorReadDto = mapper.Map<ReadAuthorDto>(author);

        return ResultBuilder.SuccessResult(authorReadDto);
    }
}