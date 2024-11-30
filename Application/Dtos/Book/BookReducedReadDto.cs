﻿namespace Application.Dtos.Book;

/// <summary>
/// Dto for Book reduced Read operation
/// </summary>
/// <param name="Id">Guid identifier of Book</param>
/// <param name="Title">string which contains Title of Book</param>
public record BookReducedReadDto(
    Guid Id,
    string Title);