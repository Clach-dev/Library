using Application.Common.Dtos;
using Application.Common.Dtos.Author;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.AuthorCases.Queries.GetAuthorsByNameCase;

public record GetAuthorsByNameQuery(
    string? LastName,
    string? FirstName,
    PageInfoDto PageInfoDto)
    : IRequest<Result<IEnumerable<ReadAuthorDto>>>;