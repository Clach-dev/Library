using Application.Dtos.Author;
using Application.Dtos.Book;
using Application.Dtos.Genre;
using Application.Utils;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.BookCases.Queries.GetBooksByFilterCase;

public record GetBooksByFilterQuery(
    string? Title,
    IEnumerable<Guid> Genres,
    IEnumerable<Guid> Authors)
    : IRequest<Result<IEnumerable<BookReadDto>>>;
