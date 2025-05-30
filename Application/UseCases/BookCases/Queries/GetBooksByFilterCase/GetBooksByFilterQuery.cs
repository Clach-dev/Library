using Application.Common.Dtos;
using Application.Common.Dtos.Book;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.BookCases.Queries.GetBooksByFilterCase;

public record GetBooksByFilterQuery(
    string? Title,
    byte? LowerAgeLimit,
    byte? UpperAgeLimit,
    IEnumerable<Guid> AuthorsIds,
    IEnumerable<Guid> GenresIds,
    PageInfoDto PageInfoDto)
    : IRequest<Result<ReadBooksDto>>;
