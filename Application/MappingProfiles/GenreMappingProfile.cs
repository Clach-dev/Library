using Application.Dtos.Genre;
using Application.UseCases.GenreCases.Commands.CreateGenreCase;
using Application.UseCases.GenreCases.Commands.DeleteGenreCase;
using Application.UseCases.GenreCases.Commands.UpdateGenreCase;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles;

public class GenreMappingProfile : Profile
{
    public GenreMappingProfile()
    {
        CreateMap<GenreCreateDto, CreateGenreCommand>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        
        CreateMap<CreateGenreCommand, Genre>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        
        CreateMap<Genre, GenreReadDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        
        CreateMap<GenreDeleteDto, DeleteGenreCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        
        CreateMap<GenreUpdateDto, UpdateGenreCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        
        CreateMap<UpdateGenreCommand, Genre>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}