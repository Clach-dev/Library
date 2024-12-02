using Application.Dtos.Book;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.BookCases.Queries.GetBookByIdCase;

public class GetBookByIdHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetBookByIdQuery, Result<ReadBookDto>>
{
    public async Task<Result<ReadBookDto>> Handle(
        GetBookByIdQuery getBookByIdQuery,
        CancellationToken cancellationToken)
    {
        var book = await unitOfWork.Books.GetByIdAsync(getBookByIdQuery.Id, cancellationToken);
        if (book is null)
        {
            ResultBuilder.NotFoundResult<ReadBookDto>(ErrorMessages.BookIdNotFound);
        }
        
        var bookReadDto = mapper.Map<ReadBookDto>(book);

        return ResultBuilder.SuccessResult(bookReadDto);
    }
}