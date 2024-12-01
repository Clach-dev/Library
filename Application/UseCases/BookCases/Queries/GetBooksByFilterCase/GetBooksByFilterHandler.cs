﻿using Application.Dtos.Book;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.BookCases.Queries.GetBooksByFilterCase;

public class GetBooksByFilterHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetBooksByFilterQuery, Result<IEnumerable<BookReadDto>>>
{
    public async Task<Result<IEnumerable<BookReadDto>>> Handle(
        GetBooksByFilterQuery getBooksByFilterQuery,
        CancellationToken cancellationToken)
    {
        var books = unitOfWork.Books.GetByPredicateAsync(book =>
                (getBooksByFilterQuery.Title == null ||
                    book.Title.Contains(getBooksByFilterQuery.Title)) ||
                (!getBooksByFilterQuery.Authors.Any() ||
                    getBooksByFilterQuery.Authors.All(authorId =>
                        book.Authors != null &&
                        book.Authors.Select(b => b.Id).Contains(authorId))) ||
                (!getBooksByFilterQuery.Genres.Any() || 
                     getBooksByFilterQuery.Genres.All(genreId =>
                         book.Genres != null &&
                         book.Genres.Select(b => b.Id).Contains(genreId))),
            cancellationToken);
        
        var booksReadDto = mapper.Map<IEnumerable<BookReadDto>>(await books);

        return ResultBuilder.SuccessResult(booksReadDto);
    }
}