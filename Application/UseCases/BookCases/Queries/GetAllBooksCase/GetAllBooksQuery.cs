using Application.Common.Dtos;
using Application.Common.Dtos.Book;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.BookCases.Queries.GetAllBooksCase;

public record GetAllBooksQuery(
    PageInfoDto PageInfoDto)
    : IRequest<Result<IEnumerable<ReadBookDto>>>;