using Application.Dtos.Book;
using Application.UseCases.BookCases.Commands.CreateBookCase;
using Application.UseCases.BookCases.Commands.DeleteBookCase;
using Application.UseCases.BookCases.Commands.UpdateBookCase;
using Application.UseCases.BookCases.Queries.GetBooksByFilterCase;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<BookCreateDto, CreateBookCommand>()
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors));
        
        CreateMap<CreateBookCommand, Book>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors));

        CreateMap<Book, BookReadDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        
        CreateMap<BookDeleteDto, DeleteBookCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        
        CreateMap<BookUpdateDto, UpdateBookCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors));
        
        CreateMap<UpdateBookCommand, Book>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors));
        
        CreateMap<BookFilterDto, GetBooksByFilterQuery>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres));
    }
}