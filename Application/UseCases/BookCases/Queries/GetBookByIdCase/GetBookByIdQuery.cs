using Application.Common.Dtos.Book;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.BookCases.Queries.GetBookByIdCase;

public record GetBookByIdQuery(
    Guid Id)
    : IRequest<Result<ReadBookDto>>;