using Application.Dtos.Book;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.BookCases.Commands.UpdateBookCase;

public class UpdateBookHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) 
    : IRequestHandler<UpdateBookCommand, Result<BookReadDto>>
{
    public async Task<Result<BookReadDto>> Handle(
        UpdateBookCommand updateBookCommand,
        CancellationToken cancellationToken)
    {
        var currentBook = await unitOfWork.Books.GetByIdAsync(updateBookCommand.Id, cancellationToken);

        if (currentBook is null)
        {
            return ResultBuilder.NotFoundResult<BookReadDto>(ErrorMessages.BookIdNotFound);
        }
        
        var existedBook = (await unitOfWork
            .Books
            .GetByPredicateAsync(book => book.ISBN == updateBookCommand.ISBN, cancellationToken))
            .FirstOrDefault();
        
        if (existedBook is not null && existedBook.Id != currentBook.Id)
        {
            ResultBuilder.ConflictResult<BookReadDto>(ErrorMessages.ExistingBookError);
        }
        
        mapper.Map(updateBookCommand, currentBook);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var bookReadDto = mapper.Map<BookReadDto>(currentBook);
        return ResultBuilder.SuccessResult(bookReadDto);
    }
}