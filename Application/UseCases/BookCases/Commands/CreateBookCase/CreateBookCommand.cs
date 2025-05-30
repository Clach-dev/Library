using Application.Common.Dtos.Book;
using Application.Common.Utils;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.BookCases.Commands.CreateBookCase;

public record CreateBookCommand(
    string ISBN,
    string Title,
    byte AgeLimit,
    string? Description,
    IEnumerable<IFormFile> Images,
    IEnumerable<Guid> GenresIds,
    IEnumerable<Guid> AuthorsIds)
    : IRequest<Result<ReadBookDto>>;