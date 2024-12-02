using Application.Common.Dtos;
using Application.Common.Dtos.Book;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.BookCases.Queries.GetAllBooksCase;

public record GetAllBooksQuery(
    PageInfo PageInfo)
    : IRequest<Result<IEnumerable<ReadBookDto>>>;