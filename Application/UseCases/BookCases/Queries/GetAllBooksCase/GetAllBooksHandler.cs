using Application.Common.Dtos.Book;
using Application.Common.Interfaces.IRepositories;
using Application.Common.Utils;
using AutoMapper;
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
        var books = await unitOfWork.Books.GetAllAsync(getAllBooksQuery, cancellationToken);

        var booksReadDto = mapper.Map<IEnumerable<ReadBookDto>>(books);

        return ResultBuilder.SuccessResult(booksReadDto);
    }
}