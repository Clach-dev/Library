using Application.Common.Dtos.Book;
using Application.Common.Utils;
using AutoMapper;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.BookCases.Commands.UpdateBookCase;

public class UpdateBookHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) 
    : IRequestHandler<UpdateBookCommand, Result<ReadBookDto>>
{
    public async Task<Result<ReadBookDto>> Handle(
        UpdateBookCommand updateBookCommand,
        CancellationToken cancellationToken)
    {
        var currentBook = await unitOfWork.Books.GetByIdAsync(updateBookCommand.Id, cancellationToken);
        if (currentBook is null)
        {
            return ResultBuilder.NotFoundResult<ReadBookDto>(ErrorMessages.BookIdNotFound);
        }
        
        var existedBook = (await unitOfWork
            .Books
            .GetByPredicateAsync(book => book.ISBN == updateBookCommand.ISBN, cancellationToken))
            .FirstOrDefault();
        if (existedBook is not null && existedBook.Id != currentBook.Id)
        {
            ResultBuilder.ConflictResult<ReadBookDto>(ErrorMessages.ExistingBookError);
        }
        
        var authors = (await unitOfWork.Authors.GetByPredicateAsync(author =>
                updateBookCommand.AuthorsIds.Contains(author.Id),
            cancellationToken)).ToList();
        if (authors.Count() != updateBookCommand.AuthorsIds.Count())
        {
            return ResultBuilder.NotFoundResult<ReadBookDto>(ErrorMessages.AuthorIdNotFound);
        }

        var genres = (await unitOfWork.Genres.GetByPredicateAsync(genre =>
                updateBookCommand.GenresIds.Contains(genre.Id), 
            cancellationToken)).ToList();
        if (genres.Count() != updateBookCommand.GenresIds.Count())
        {
            return ResultBuilder.NotFoundResult<ReadBookDto>(ErrorMessages.GenreIdNotFound);
        }
        
        mapper.Map(updateBookCommand, currentBook);
        currentBook.Authors = authors;
        currentBook.Genres = genres;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var bookReadDto = mapper.Map<ReadBookDto>(currentBook);
        return ResultBuilder.SuccessResult(bookReadDto);
    }
}