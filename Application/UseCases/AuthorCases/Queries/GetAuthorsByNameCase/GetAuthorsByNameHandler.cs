using Application.Dtos.Author;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.AuthorCases.Queries.GetAuthorsByNameCase;

public class GetAuthorsByNameHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetAuthorsByNameQuery, Result<IEnumerable<ReadAuthorDto>>>
{
    public async Task<Result<IEnumerable<ReadAuthorDto>>> Handle(
        GetAuthorsByNameQuery getAuthorsByNameQuery,
        CancellationToken cancellationToken)
    {
        var searchFirstName = getAuthorsByNameQuery.FirstName?.ToLower() ?? string.Empty;
        var searchLastName = getAuthorsByNameQuery.LastName?.ToLower() ?? string.Empty;
        
        var authors = await unitOfWork.Authors.GetByPredicateAsync(
            author =>
                (author.FirstName.ToLower().Contains(searchFirstName) ||
                 author.LastName.ToLower().Contains(searchFirstName)) &&
                (author.FirstName.ToLower().Contains(searchLastName) ||
                 author.LastName.ToLower().Contains(searchLastName)),
            cancellationToken);
        
        var genresReadDtos = mapper.Map<IEnumerable<ReadAuthorDto>>(authors);
        
        return ResultBuilder.SuccessResult(genresReadDtos);
    }
}