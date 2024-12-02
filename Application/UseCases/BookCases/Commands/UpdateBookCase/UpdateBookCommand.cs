using Application.Dtos.Book;
using Application.Utils;
using MediatR;

namespace Application.UseCases.BookCases.Commands.UpdateBookCase;

public record UpdateBookCommand(
    Guid Id,
    string? ISBN,
    string? Title,
    string? Description,
    IEnumerable<Guid> GenresIds,
    IEnumerable<Guid> AuthorsIds)
    : IRequest<Result<ReadBookDto>>;