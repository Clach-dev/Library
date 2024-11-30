using Application.Dtos.Author;
using Application.Utils;
using MediatR;

namespace Application.UseCases.AuthorCases.Queries.GetAuthorByNameCase;

public record GetAuthorByNameQuery(
    string? LastName,
    string? FirstName)
    : IRequest<Result<IEnumerable<AuthorReadDto>>>;