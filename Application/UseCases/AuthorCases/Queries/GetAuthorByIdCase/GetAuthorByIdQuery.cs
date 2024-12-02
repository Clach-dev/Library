using Application.Common.Dtos.Author;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.AuthorCases.Queries.GetAuthorByIdCase;

public record GetAuthorByIdQuery(
    Guid Id)
    : IRequest<Result<ReadAuthorDto>>;