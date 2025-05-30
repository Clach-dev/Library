using Application.Common.Dtos.Book;
using FluentValidation;

namespace Presentation.Common.Validators.Book;

public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
{
    public CreateBookDtoValidator()
    {
        RuleFor(x => x.ISBN)
            .NotNullRule()
            .ISBNRule();
        
        RuleFor(x => x.Title)
            .NotNullRule()
            .TitleRule();

        RuleFor(x => x.AgeLimit)
            .AgeLimitRule();
        
        RuleFor(x => x.Description)
            .DescriptionRule();
        
        RuleFor(x => x.Images)
            .ImagesRule();
        
        RuleFor(x => x.GenresIds)
            .GuidListRule();
        
        RuleFor(x => x.AuthorsIds)
            .GuidListRule();
    }
}