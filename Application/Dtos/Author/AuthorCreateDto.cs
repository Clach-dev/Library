﻿namespace Application.Dtos.Author;

/// <summary>
/// DTO for Author Create operation
/// </summary>
/// <param name="LastName">string which contains Last name of Author</param>
/// <param name="FirstName">string which contains First name of Author</param>
/// <param name="MiddleName">string which contains Middle name of Author</param>
/// <param name="Description">string which contains Description of Author</param>
public record AuthorCreateDto(
    string LastName,
    string FirstName,
    string? MiddleName,
    string? Description);