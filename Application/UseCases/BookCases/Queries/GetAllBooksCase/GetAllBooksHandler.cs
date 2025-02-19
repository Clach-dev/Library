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
    : IRequestHandler<GetAllBooksQuery, Result<IEnumerable<ReadBookDto>>>
{
    public async Task<Result<IEnumerable<ReadBookDto>>> Handle(
        GetAllBooksQuery getAllBooksQuery,
        CancellationToken cancellationToken)
    {
        var books = await unitOfWork.Books.GetAllAsync(mapper.Map<PageInfo>(getAllBooksQuery.PageInfoDto), cancellationToken);

        var booksReadDto = mapper.Map<IEnumerable<ReadBookDto>>(books);

        return ResultBuilder.SuccessResult(booksReadDto);
    }
}