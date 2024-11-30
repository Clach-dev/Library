using Application.Dtos.Author;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.AuthorCases.Queries.GetAllAuthorsCase;

public class GetAllAuthorsHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllAuthorsQuery ,Result<IEnumerable<AuthorReadDto>>>
{
    public async Task<Result<IEnumerable<AuthorReadDto>>> Handle(
        GetAllAuthorsQuery getAllAuthorsQuery,
        CancellationToken cancellationToken)
    {
        var authors = await unitOfWork.Authors.GetAllAsync(getAllAuthorsQuery, cancellationToken);
        
        var genresReadDtos = mapper.Map<IEnumerable<AuthorReadDto>>(authors);
        
        return ResultBuilder.SuccessResult(genresReadDtos);
    }
}