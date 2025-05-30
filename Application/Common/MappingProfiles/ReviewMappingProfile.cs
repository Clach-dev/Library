using Application.Common.Dtos.Review;
using Application.UseCases.ReviewCases.Commands.CreateReviewCase;
using Application.UseCases.ReviewCases.Commands.DeleteReviewCase;
using Application.UseCases.ReviewCases.Commands.UpdateReviewCase;
using Application.UseCases.ReviewCases.Queries.GetReviewsByFilterCase;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class ReviewMappingProfile : Profile
{
    public ReviewMappingProfile()
    {
        CreateMap<CreateReviewDto, CreateReviewCommand>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => Guid.Empty))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment));
        
        CreateMap<CreateReviewCommand, Review>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment));

        CreateMap<Review, ReadReviewDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment));
        
        CreateMap<DeleteReviewDto, DeleteReviewCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<UpdateReviewDto, UpdateReviewCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment));
        
        CreateMap<UpdateReviewCommand, Review>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment));
            
        CreateMap<Review, ReadReviewReducedDto>() // TODO maybe remove this mapping
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating));

        CreateMap<FilterReviewsDto, GetReviewsByFilterQuery>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.WithComment, opt => opt.MapFrom(src => src.WithComment))
            .ForMember(dest => dest.PageInfoDto, opt => opt.MapFrom(src => src.PageInfoDto));
    }
}