using Application.Common.Dtos.Book;
using Application.Common.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
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
        var existedBook = (await unitOfWork.Books.GetByPredicateAsync(
                    book => book.ISBN == createBookCommand.ISBN,
                    new PageInfo(),
                    cancellationToken))
            .Item1.FirstOrDefault();
        if (existedBook is not null)
        {
            return ResultBuilder.ConflictResult<ReadBookDto>(ErrorMessages.ExistingBookError);
        }

        var authors = (await unitOfWork.Authors.GetByPredicateAsync(
            author => createBookCommand.AuthorsIds.Contains(author.Id),
            new PageInfo(1, createBookCommand.AuthorsIds.Count()), 
            cancellationToken))
            .Item1.ToList();
        if (authors.Count != createBookCommand.AuthorsIds.Count())
        {
            return ResultBuilder.NotFoundResult<ReadBookDto>(ErrorMessages.AuthorIdNotFound);
        }

        var genres = (await unitOfWork.Genres.GetByPredicateAsync(
                genre => createBookCommand.GenresIds.Contains(genre.Id), 
                new PageInfo(1, createBookCommand.GenresIds.Count()),
                cancellationToken))
            .Item1.ToList();
        if (genres.Count != createBookCommand.GenresIds.Count())
        {
            return ResultBuilder.NotFoundResult<ReadBookDto>(ErrorMessages.GenreIdNotFound);
        }

        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        var uploadedImageUris = new List<Uri>();

        try
        {
            var newBook = mapper.Map<Book>(createBookCommand);
            newBook.Authors = authors;
            newBook.Genres = genres;

            foreach (var image in createBookCommand.Images)
            {
                await using var imageStream = image.OpenReadStream();
                var imageUri = await unitOfWork.BookImages.UploadFileAsync(imageStream, cancellationToken);

                uploadedImageUris.Add(imageUri);
            }
            newBook.Images = uploadedImageUris;
            
            await unitOfWork.Books.CreateAsync(newBook, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            
            newBook.Images = await unitOfWork.BookImages.GetReadOnlyImageUrisAsync(newBook.Images);
            
            var bookDto = mapper.Map<ReadBookDto>(newBook);
            return ResultBuilder.CreatedResult(bookDto);
        }
        catch
        {
            foreach (var imageUri in uploadedImageUris)
            {
                await unitOfWork.BookImages.DeleteFileAsync(imageUri, cancellationToken);
            }
            
            return ResultBuilder.InternalServerErrorResult<ReadBookDto>(ErrorMessages.BookCreationFailureError);
        }
    }
}
