using Application.Dtos;
using Application.Dtos.Book;
using Application.Utils;
using MediatR;

namespace Application.UseCases.BookCases.Queries.GetAllBooksCase;

public record GetAllBooksQuery()
    : PageInfo, IRequest<Result<IEnumerable<BookReadDto>>>;