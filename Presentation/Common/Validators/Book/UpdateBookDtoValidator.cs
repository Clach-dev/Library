using Application.Common.Dtos.Book;
using FluentValidation;

namespace Presentation.Common.Validators.Book;

public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
{
    public UpdateBookDtoValidator()
    {
        RuleFor(x => x.Id)
            .GuidRule();

        RuleFor(x => x.Title)
            .TitleRule();

        RuleFor(x => x.AgeLimit)
            .AgeLimitRule();

        RuleFor(x => x.Description)
            .DescriptionRule();
        
        RuleFor(x => x.NewImages)
            .ImagesRule();
        
        RuleFor(x => x.KeepImageUris)
            .UrisRule();
        
        RuleFor(x => x.GenresIds)
            .GuidListRule();
        
        RuleFor(x => x.AuthorsIds)
            .GuidListRule();
    }
}