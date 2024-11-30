using Application.Dtos.Author;
using Application.Utils;
using MediatR;

namespace Application.UseCases.AuthorCases.Queries.GetAuthorByIdCase;

public record GetAuthorByIdQuery(
    Guid Id)
    : IRequest<Result<AuthorReadDto>>;