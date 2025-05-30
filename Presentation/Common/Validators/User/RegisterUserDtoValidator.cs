using Application.Common.Dtos.User;
using FluentValidation;

namespace Presentation.Common.Validators.User;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotNullRule()
            .PhoneNumberRule();

        RuleFor(x => x.Password)
            .NotNullRule()
            .PasswordRule();

        RuleFor(x => x.LastName)
            .NotNullRule()
            .LastNameRule();

        RuleFor(x => x.FirstName)
            .NotNullRule()
            .FirstNameRule();

        RuleFor(x => x.MiddleName)
            .MiddleNameRule();

        RuleFor(x => x.BirthDate)
            .BirthDateRule();

        RuleFor(x => x.ProfileImage)
            .ProfileImageRule();
    }
}