using Application.Dtos.Book;
using Application.Utils;
using MediatR;

namespace Application.UseCases.BookCases.Commands.CreateBookCase;

public record CreateBookCommand(
    string ISBN,
    string Title,
    string? Description,
    IEnumerable<Guid> GenresIds,
    IEnumerable<Guid> AuthorsIds)
    : IRequest<Result<ReadBookDto>>;