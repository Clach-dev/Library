using Application.Common.Dtos.Author;
using Application.Common.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.AuthorCases.Queries.GetAuthorsByNameCase;

public class GetAuthorsByNameHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetAuthorsByNameQuery, Result<ReadAuthorsDto>>
{
    public async Task<Result<ReadAuthorsDto>> Handle(
        GetAuthorsByNameQuery getAuthorsByNameQuery,
        CancellationToken cancellationToken)
    {
        var searchFirstName = getAuthorsByNameQuery.FirstName?.ToLower() ?? string.Empty;
        var searchLastName = getAuthorsByNameQuery.LastName?.ToLower() ?? string.Empty;
        
        var authors = await unitOfWork.Authors.GetByPredicateAsync(
            author =>
                (author.FirstName.Contains(searchFirstName, StringComparison.CurrentCultureIgnoreCase) ||
                 author.LastName.Contains(searchFirstName, StringComparison.CurrentCultureIgnoreCase)) &&
                (author.FirstName.Contains(searchLastName, StringComparison.CurrentCultureIgnoreCase) ||
                 author.LastName.Contains(searchLastName, StringComparison.CurrentCultureIgnoreCase)),
            mapper.Map<PageInfo>(getAuthorsByNameQuery.PageInfoDto),
            cancellationToken);
        
        var authorsReadDtos = new ReadAuthorsDto(mapper.Map<IEnumerable<ReadAuthorDto>>(authors.Item1), authors.Item2);
        
        return ResultBuilder.SuccessResult(authorsReadDtos);
    }
}