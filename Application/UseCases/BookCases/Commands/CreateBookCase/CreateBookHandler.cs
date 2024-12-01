using Application.Dtos.Book;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.BookCases.Commands.CreateBookCase;

public class CreateBookHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateBookCommand, Result<BookReadDto>>
{
    public async Task<Result<BookReadDto>> Handle(
        CreateBookCommand createBookCommand,
        CancellationToken cancellationToken)
    {
        var existedBook = (await unitOfWork
            .Books
            .GetByPredicateAsync(book => book.ISBN == createBookCommand.ISBN, cancellationToken))
            .FirstOrDefault();
        
        if (existedBook is not null)
        {
            ResultBuilder.ConflictResult<BookReadDto>(ErrorMessages.ExistingBookError);
        }
        
        var newBook = mapper.Map<Book>(createBookCommand);

        await unitOfWork.Books.CreateAsync(newBook, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var userReadDto = mapper.Map<BookReadDto>(newBook);
        return ResultBuilder.CreatedResult(userReadDto);
    }
}