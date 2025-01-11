﻿using Application.Common.Dtos;
using Application.Common.Dtos.Book;
using Application.UseCases.BookCases.Commands.CreateBookCase;
using Application.UseCases.BookCases.Commands.DeleteBookCase;
using Application.UseCases.BookCases.Commands.UpdateBookCase;
using Application.UseCases.BookCases.Queries.GetAllBooksCase;
using Application.UseCases.BookCases.Queries.GetBookByIdCase;
using Application.UseCases.BookCases.Queries.GetBooksByFilterCase;
using AutoMapper;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BooksController(
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper,
    IMediator mediator)
    : CustomControllerBase(httpContextAccessor)
{
    /// <summary>
    /// Get all books operation
    /// </summary>
    /// <param name="pageInfo">PageInfo which contains number of current page and number of items per page</param>
    /// <param name="cancellationToken">CancellationToken of operation cancel</param>
    /// <returns>Result with books information</returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllBooks(
        [FromQuery] PageInfo pageInfo,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllBooksQuery(pageInfo), cancellationToken);
        return Result(result);
    }
    
    /// <summary>
    /// Get book by id operation
    /// </summary>
    /// <param name="bookId">Guid identifier of book</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result with book information</returns>
    [HttpGet("{bookId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetBookById(
        [FromRoute] Guid bookId,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetBookByIdQuery(bookId), cancellationToken);
        return Result(result);
    }

    /// <summary>
    /// Get filtered books operation
    /// </summary>
    /// <param name="readBooksByFilterDto">ReadBooksByFilterDto which contains filter information</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result with filtered books information</returns>
    [HttpGet("filter")]
    [AllowAnonymous]
    public async Task<IActionResult> GetFilteredBooks(
        [FromQuery] ReadBooksByFilterDto readBooksByFilterDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<GetBooksByFilterQuery>(readBooksByFilterDto), cancellationToken);
        return Result(result);
    }
    
    /// <summary>
    /// Book create operation
    /// </summary>
    /// <param name="createBookDto">CreateBookDto which contains book information</param>
    /// <param name="cancellationToken">CancellationToken of operation cancel</param>
    /// <returns>Result with created book information</returns>
    [HttpPost]
    [Authorize(Policy = Policies.OnlyAdminAccess)]
    public async Task<IActionResult> CreateBook(
        [FromBody] CreateBookDto createBookDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<CreateBookCommand>(createBookDto), cancellationToken);
        return Result(result);
    }
    
    /// <summary>
    /// Book update operation
    /// </summary>
    /// <param name="updateBookDto">UpdateBookDto which contains book update information</param>
    /// <param name="cancellationToken">CancellationToken of operation cancel</param>
    /// <returns>Result with updated book information</returns>
    [HttpPut]
    [Authorize(Policy = Policies.OnlyAdminAccess)]
    public async Task<IActionResult> UpdateBook(
        [FromBody] UpdateBookDto updateBookDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<UpdateBookCommand>(updateBookDto), cancellationToken);
        return Result(result);
    }
    
    /// <summary>
    /// Book delete operation
    /// </summary>
    /// <param name="deleteBookDto">DeleteBookDto which contains id of book</param>
    /// <param name="cancellationToken">CancellationToken of operation cancel</param>
    /// <returns>Result with status code of delete operation</returns>
    [HttpDelete]
    [Authorize(Policy = Policies.OnlyAdminAccess)]
    public async Task<IActionResult> DeleteBook(
        [FromBody] DeleteBookDto deleteBookDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<DeleteBookCommand>(deleteBookDto), cancellationToken);
        return Result(result);
    }
}