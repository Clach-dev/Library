using Application.Common.Dtos.Book;
using Application.UseCases.BookCases.Commands.CreateBookCase;
using Application.UseCases.BookCases.Commands.DeleteBookCase;
using Application.UseCases.BookCases.Commands.UpdateBookCase;
using Application.UseCases.BookCases.Queries.GetBooksByFilterCase;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<CreateBookDto, CreateBookCommand>()
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.AgeLimit, opt => opt.MapFrom(src => src.AgeLimit))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.GenresIds, opt => opt.MapFrom(src => src.GenresIds))
            .ForMember(dest => dest.AuthorsIds, opt => opt.MapFrom(src => src.AuthorsIds));
        
        CreateMap<CreateBookCommand, Book>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.AgeLimit, opt => opt.MapFrom(src => src.AgeLimit))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Images, opt => opt.Ignore())
            .ForMember(dest => dest.Genres, opt => opt.Ignore())
            .ForMember(dest => dest.Authors, opt => opt.Ignore());

        CreateMap<Book, ReadBookDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.AgeLimit, opt => opt.MapFrom(src => src.AgeLimit))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));
        
        CreateMap<DeleteBookDto, DeleteBookCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        
        CreateMap<UpdateBookDto, UpdateBookCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.AgeLimit, opt => opt.MapFrom(src => src.AgeLimit))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.NewImages, opt => opt.MapFrom(src => src.NewImages))
            .ForMember(dest => dest.KeepImageUris, opt => opt.MapFrom(src => src.KeepImageUris))
            .ForMember(dest => dest.GenresIds, opt => opt.MapFrom(src => src.GenresIds))
            .ForMember(dest => dest.AuthorsIds, opt => opt.MapFrom(src => src.AuthorsIds));
        
        CreateMap<UpdateBookCommand, Book>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.AgeLimit, opt => opt.MapFrom(src => src.AgeLimit))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Images, opt => opt.Ignore())
            .ForMember(dest => dest.Genres, opt => opt.Ignore())
            .ForMember(dest => dest.Authors, opt => opt.Ignore());
        
        CreateMap<Book, ReadBookReducedDto>() // TODO MAYBE DELETE if not used
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));

        CreateMap<FilterBooksDto, GetBooksByFilterQuery>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.LowerAgeLimit, opt => opt.MapFrom(src => src.LowerAgeLimit))
            .ForMember(dest => dest.UpperAgeLimit, opt => opt.MapFrom(src => src.UpperAgeLimit))
            .ForMember(dest => dest.AuthorsIds, opt => opt.MapFrom(src => src.AuthorsIds))
            .ForMember(dest => dest.GenresIds, opt => opt.MapFrom(src => src.GenresIds))
            .ForMember(dest => dest.PageInfoDto, opt => opt.MapFrom(src => src.PageInfoDto));
    }
}