using Application.Common.Dtos.User;
using FluentValidation;

namespace Presentation.Common.Validators.User;

public class AuthUserDtoValidator : AbstractValidator<AuthUserDto>
{
    public AuthUserDtoValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotNullRule()
            .PhoneNumberRule();
        
        RuleFor(x => x.Password)
            .NotNullRule()
            .PasswordRule();
    }
}