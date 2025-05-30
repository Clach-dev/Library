using Application.Common.Dtos.Book;
using Application.Common.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.BookCases.Queries.GetBooksByFilterCase;

public class GetBooksByFilterHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetBooksByFilterQuery, Result<ReadBooksDto>>
{
    public async Task<Result<ReadBooksDto>> Handle(
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
                        book.Genres.Select(b => b.Id).Contains(genreId))) &&
                (getBooksByFilterQuery.LowerAgeLimit == null || 
                    getBooksByFilterQuery.LowerAgeLimit <= book.AgeLimit) &&
                (getBooksByFilterQuery.UpperAgeLimit == null ||
                    getBooksByFilterQuery.UpperAgeLimit >= book.AgeLimit),
            mapper.Map<PageInfo>(getBooksByFilterQuery.PageInfoDto),
            cancellationToken);
        
        var booksReadDto = mapper.Map<ReadBooksDto>(books);

        return ResultBuilder.SuccessResult(booksReadDto);
    }
}