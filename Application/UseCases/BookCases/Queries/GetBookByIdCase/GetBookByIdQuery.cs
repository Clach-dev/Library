using Application.Dtos.Book;
using Application.Utils;
using MediatR;

namespace Application.UseCases.BookCases.Queries.GetBookByIdCase;

public record GetBookByIdQuery(
    Guid Id)
    : IRequest<Result<ReadBookDto>>;