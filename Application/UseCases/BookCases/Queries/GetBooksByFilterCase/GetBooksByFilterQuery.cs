using Application.Common.Dtos;
using Application.Common.Dtos.Book;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.BookCases.Queries.GetBooksByFilterCase;

public record GetBooksByFilterQuery(
    string? Title,
    IEnumerable<Guid> GenresIds,
    IEnumerable<Guid> AuthorsIds,
    PageInfo PageInfo)
    : IRequest<Result<IEnumerable<ReadBookDto>>>;
