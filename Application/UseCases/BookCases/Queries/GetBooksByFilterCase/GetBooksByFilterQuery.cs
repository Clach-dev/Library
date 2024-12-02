using Application.Dtos.Book;
using Application.Utils;
using MediatR;

namespace Application.UseCases.BookCases.Queries.GetBooksByFilterCase;

public record GetBooksByFilterQuery(
    string? Title,
    IEnumerable<Guid> GenresIds,
    IEnumerable<Guid> AuthorsIds)
    : IRequest<Result<IEnumerable<ReadBookDto>>>;
