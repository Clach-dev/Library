using Application.Dtos;
using Application.Dtos.Author;
using Application.Utils;
using MediatR;

namespace Application.UseCases.AuthorCases.Queries.GetAllAuthorsCase;

public record GetAllAuthorsQuery
    : PageInfo, IRequest<Result<IEnumerable<AuthorReadDto>>>;