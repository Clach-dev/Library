using Application.Common.Dtos.Book;
using Application.Common.Utils;
using AutoMapper;
using Domain.Entities;
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
            .GetByPredicateAsync(
                book => book.ISBN == updateBookCommand.ISBN, 
                new PageInfo(), 
                cancellationToken))
            .Item1.FirstOrDefault();
        if (existedBook is not null && existedBook.Id != currentBook.Id)
        {
            return ResultBuilder.ConflictResult<ReadBookDto>(ErrorMessages.ExistingBookError);
        }
        
        var pi = new PageInfo(1, updateBookCommand.AuthorsIds.Count());
        
        var authors = (await unitOfWork.Authors.GetByPredicateAsync(
            author => updateBookCommand.AuthorsIds.Contains(author.Id),
            new PageInfo(1, updateBookCommand.AuthorsIds.Count()),
            cancellationToken))
            .Item1.ToList();
        if (authors.Count != updateBookCommand.AuthorsIds.Count())
        {
            return ResultBuilder.NotFoundResult<ReadBookDto>(ErrorMessages.AuthorIdNotFound);
        }

        var genres = (await unitOfWork.Genres.GetByPredicateAsync(
            genre => updateBookCommand.GenresIds.Contains(genre.Id), 
            new PageInfo(1, updateBookCommand.GenresIds.Count()),
            cancellationToken))
            .Item1.ToList();
        if (genres.Count != updateBookCommand.GenresIds.Count())
        {
            return ResultBuilder.NotFoundResult<ReadBookDto>(ErrorMessages.GenreIdNotFound);
        }
        
        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        var uploadedUris = new List<Uri>();

        try
        {
            mapper.Map(updateBookCommand, currentBook);
            currentBook.Authors = authors;
            currentBook.Genres = genres;

            var keepUris = updateBookCommand.KeepImageUris.Select(uri => new Uri(uri.GetLeftPart(UriPartial.Path))).ToList();
            var currentUris = currentBook.Images.ToList();

            var urisToDelete = currentUris.Where(oldUri =>
                !keepUris.Contains(new Uri(oldUri.GetLeftPart(UriPartial.Path)))).ToList();
            foreach (var uri in urisToDelete)
            {
                await unitOfWork.BookImages.DeleteFileAsync(uri, cancellationToken);
            }

            foreach (var image in updateBookCommand.NewImages)
            {
                    await using var stream = image.OpenReadStream();
                    var newUri = await unitOfWork.BookImages.UploadFileAsync(stream, cancellationToken);
                    uploadedUris.Add(newUri);
            }
            currentBook.Images = keepUris.Concat(uploadedUris).ToList();

            await unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            currentBook.Images = await unitOfWork.BookImages.GetReadOnlyImageUrisAsync(currentBook.Images);
            var resultDto = mapper.Map<ReadBookDto>(currentBook);

            return ResultBuilder.SuccessResult(resultDto);
        }
        catch
        {
            foreach (var uri in uploadedUris)
            {
                await unitOfWork.BookImages.DeleteFileAsync(uri, cancellationToken);
            }

            return ResultBuilder.InternalServerErrorResult<ReadBookDto>(ErrorMessages.BookUpdateFailureError);
        }        
    }
}