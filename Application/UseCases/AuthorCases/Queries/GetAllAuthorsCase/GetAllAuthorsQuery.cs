using Application.Common.Dtos;
using Application.Common.Dtos.Author;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.AuthorCases.Queries.GetAllAuthorsCase;

public record GetAllAuthorsQuery(
    PageInfo PageInfo)
    : IRequest<Result<IEnumerable<ReadAuthorDto>>>;