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
        var searchFirstName = getAuthorByNameQuery.FirstName?.ToLower() ?? string.Empty;
        var searchLastName = getAuthorByNameQuery.LastName?.ToLower() ?? string.Empty;
        
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