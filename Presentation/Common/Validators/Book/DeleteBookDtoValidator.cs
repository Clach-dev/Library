using Application.Common.Dtos.Book;
using FluentValidation;

namespace Presentation.Common.Validators.Book;

public class DeleteBookDtoValidator : AbstractValidator<DeleteBookDto>
{
    public DeleteBookDtoValidator()
    {
        RuleFor(x => x.Id)
            .GuidRule();
    }
}