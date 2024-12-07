using Application.Common.Dtos.Author;
using FluentValidation;

namespace Presentation.Common.Validators.Author;

public class DeleteAuthorDtoValidator : AbstractValidator<DeleteAuthorDto>
{
    public DeleteAuthorDtoValidator() 
    {
        RuleFor(x => x.Id)
            .GuidRule();
    }
}