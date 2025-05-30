    using Microsoft.AspNetCore.Http;

    namespace Application.Common.Dtos.User;

    /// <summary>
    /// DTO for User Registration operation
    /// </summary>
    /// <param name="PhoneNumber">string which contains User phone number</param>
    /// <param name="Password">string which contains User password</param>
    /// <param name="LastName">string which contains Last name of User</param>
    /// <param name="FirstName">string which contains First name of User</param>
    /// <param name="MiddleName">string which contains Middle name of User</param>
    /// <param name="BirthDate">DateTime which contains Birth date of User. Format: dd/mm/yyyy</param>
    /// <param name="ProfileImage">IFormFile which contains User profile image</param>
    public record RegisterUserDto(
        string PhoneNumber,
        string Password,
        string LastName,
        string FirstName,
        string? MiddleName,
        DateTime BirthDate,
        IFormFile? ProfileImage);