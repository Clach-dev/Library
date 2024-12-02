using Application.Common.Dtos.Book;
using Application.Common.Interfaces.IRepositories;
using Application.Common.Utils;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.BookCases.Commands.CreateBookCase;

public class CreateBookHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateBookCommand, Result<ReadBookDto>>
{
    public async Task<Result<ReadBookDto>> Handle(
        CreateBookCommand createBookCommand,
        CancellationToken cancellationToken)
    {
        var existedBook = (await unitOfWork
            .Books
            .GetByPredicateAsync(book => book.ISBN == createBookCommand.ISBN, cancellationToken))
            .FirstOrDefault();
        if (existedBook is not null)
        {
            ResultBuilder.ConflictResult<ReadBookDto>(ErrorMessages.ExistingBookError);
        }
        
        var authors = (await unitOfWork.Authors.GetByPredicateAsync(author =>
                createBookCommand.AuthorsIds.Contains(author.Id),
            cancellationToken)).ToList();
        if (authors.Count() != createBookCommand.AuthorsIds.Count())
        {
            return ResultBuilder.NotFoundResult<ReadBookDto>(ErrorMessages.AuthorIdNotFound);
        }

        var genres = (await unitOfWork.Genres.GetByPredicateAsync(genre =>
                createBookCommand.GenresIds.Contains(genre.Id), 
            cancellationToken)).ToList();
        if (genres.Count() != createBookCommand.GenresIds.Count())
        {
            return ResultBuilder.NotFoundResult<ReadBookDto>(ErrorMessages.GenreIdNotFound);
        }
        
        var newBook = mapper.Map<Book>(createBookCommand);
        newBook.Authors = authors;
        newBook.Genres = genres;
        
        await unitOfWork.Books.CreateAsync(newBook, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var userReadDto = mapper.Map<ReadBookDto>(newBook);
        return ResultBuilder.CreatedResult(userReadDto);
    }
}