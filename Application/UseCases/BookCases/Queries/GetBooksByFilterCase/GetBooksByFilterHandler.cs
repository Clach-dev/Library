using Application.Common.Dtos.Book;
using Application.Common.Interfaces.IRepositories;
using Application.Common.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.BookCases.Queries.GetBooksByFilterCase;

public class GetBooksByFilterHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetBooksByFilterQuery, Result<IEnumerable<ReadBookDto>>>
{
    public async Task<Result<IEnumerable<ReadBookDto>>> Handle(
        GetBooksByFilterQuery getBooksByFilterQuery,
        CancellationToken cancellationToken)
    {
        var books = await unitOfWork.Books.GetByPredicateAsync(book =>
                (getBooksByFilterQuery.Title == null ||
                    book.Title.Contains(getBooksByFilterQuery.Title)) &&
                (!getBooksByFilterQuery.AuthorsIds.Any() ||
                    getBooksByFilterQuery.AuthorsIds.All(authorId =>
                        book.Authors != null &&
                        book.Authors.Select(b => b.Id).Contains(authorId))) &&
                (!getBooksByFilterQuery.GenresIds.Any() || 
                    getBooksByFilterQuery.GenresIds.All(genreId =>
                        book.Genres != null &&
                        book.Genres.Select(b => b.Id).Contains(genreId))),
            cancellationToken);
        
        var booksReadDto = mapper.Map<IEnumerable<ReadBookDto>>(books);

        return ResultBuilder.SuccessResult(booksReadDto);
    }
}