using Application.Dtos.Reservation;
using Application.UseCases.ReservationCases.Commands.CreateReservationCase;
using Application.UseCases.ReservationCases.Commands.DeleteReservationCase;
using Application.UseCases.ReservationCases.Commands.UpdateReservationCase;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles;

public class ReservationMappingProfile : Profile
{
    ReservationMappingProfile()
    {
        CreateMap<ReservationCreateDto, CreateReservationCommand>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => Guid.Empty));
        
        CreateMap<CreateReservationCommand, Reservation>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ReceiptDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src =>DateTime.Now.AddDays(30)))
            .ForMember(dest => dest.IsReturned, opt => opt.MapFrom(src => false));

        CreateMap<Reservation, ReservationReadDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ReceiptDate, opt => opt.MapFrom(src => src.ReceiptDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
            .ForMember(dest => dest.IsReturned, opt => opt.MapFrom(src => src.IsReturned))
            .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
        
        CreateMap<ReservationDeleteDto, DeleteReservationCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<ReservationUpdateDto, UpdateReservationCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ReceiptDate, opt => opt.MapFrom(src => src.ReceiptDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
            .ForMember(dest => dest.IsReturned, opt => opt.MapFrom(src => src.IsReturned));
        
        CreateMap<UpdateReservationCommand, Reservation>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ReceiptDate, opt => opt.MapFrom(src => src.ReceiptDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
            .ForMember(dest => dest.IsReturned, opt => opt.MapFrom(src => src.IsReturned));
    }
}