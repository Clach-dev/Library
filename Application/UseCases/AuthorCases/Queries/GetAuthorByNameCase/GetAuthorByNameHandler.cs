using Application.Dtos.Author;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.AuthorCases.Queries.GetAuthorByNameCase;

public class GetAuthorByNameHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetAuthorByNameQuery, Result<IEnumerable<ReadAuthorDto>>>
{
    public async Task<Result<IEnumerable<ReadAuthorDto>>> Handle(
        GetAuthorByNameQuery getAuthorByNameQuery,
        CancellationToken cancellationToken)
    {
        var authors = (await unitOfWork.
            Authors.
            GetByPredicateAsync(author => author.LastName == getAuthorByNameQuery.LastName, cancellationToken))
            .Where(author => author.FirstName == getAuthorByNameQuery.FirstName);
        
        var genresReadDtos = mapper.Map<IEnumerable<ReadAuthorDto>>(authors);
        
        return ResultBuilder.SuccessResult(genresReadDtos);
    }
}