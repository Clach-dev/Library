using Application.Common.Dtos.Genre;
using FluentValidation;

namespace Presentation.Common.Validators.Genre;

public class DeleteGenreDtoValidator : AbstractValidator<DeleteGenreDto>
{
    public DeleteGenreDtoValidator()
    {
        RuleFor(x => x.Id)
            .GuidRule();
    }
}