using Application.Dtos.Book;
using Application.Utils;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.BookCases.Commands.UpdateBookCase;

public record UpdateBookCommand(
    Guid Id,
    string? ISBN,
    string? Title,
    string? Description,
    IEnumerable<Genre> Genres,
    IEnumerable<Author> Authors)
    : IRequest<Result<BookReadDto>>;