using Application.Common.Dtos.Book;
using Application.Common.Utils;
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