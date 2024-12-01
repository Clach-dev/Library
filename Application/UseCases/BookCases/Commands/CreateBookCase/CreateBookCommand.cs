using Application.Dtos.Book;
using Application.Utils;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.BookCases.Commands.CreateBookCase;

public record CreateBookCommand(
    string ISBN,
    string Title,
    string Description,
    IEnumerable<Guid> Genres,
    IEnumerable<Guid> Authors)
    : IRequest<Result<ReadBookDto>>;