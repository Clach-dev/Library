using Application.Common.Dtos.Book;
using Application.Common.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.BookCases.Queries.GetAllBooksCase;

public class GetAllBooksHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetAllBooksQuery, Result<ReadBooksDto>>
{
    public async Task<Result<ReadBooksDto>> Handle(
        GetAllBooksQuery getAllBooksQuery,
        CancellationToken cancellationToken)
    {
        var books = await unitOfWork.Books.GetAllAsync(mapper.Map<PageInfo>(getAllBooksQuery.PageInfoDto), cancellationToken);

        var booksReadDto = new ReadBooksDto(mapper.Map<IEnumerable<ReadBookDto>>(books.Item1), books.Item2);

        return ResultBuilder.SuccessResult(booksReadDto);
    }
}