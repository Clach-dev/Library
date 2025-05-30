using Application.Common.Dtos.Book;
using Application.Common.Utils;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.BookCases.Commands.UpdateBookCase;

public record UpdateBookCommand(
    Guid Id,
    string? ISBN,
    string? Title,
    byte? AgeLimit,
    string? Description,
    IEnumerable<IFormFile> NewImages,
    IEnumerable<Uri> KeepImageUris,
    IEnumerable<Guid> GenresIds,
    IEnumerable<Guid> AuthorsIds)
    : IRequest<Result<ReadBookDto>>;    