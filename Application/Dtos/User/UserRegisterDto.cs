﻿namespace Application.Dtos.User;

/// <summary>
/// DTO for User Registration operation
/// </summary>
/// <param name="Login">string which contains User login</param>
/// <param name="Password">string which contains User password</param>
/// <param name="LastName">string which contains Last name of User</param>
/// <param name="FirstName">string which contains First name of User</param>
/// <param name="MiddleName">string which contains Middle name of User</param>
/// <param name="BirthDate">DateTime which contains Birth date of User. Format: dd.mm.yyyy</param>
public record UserRegisterDto(
    string Login,
    string Password,
    string LastName,
    string FirstName,
    string? MiddleName,
    DateTime BirthDate);