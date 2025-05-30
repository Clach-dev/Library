using Application.Common.Dtos.Reservation;
using Application.UseCases.ReservationCases.Commands.CreateReservationCase;
using Application.UseCases.ReservationCases.Commands.DeleteReservationCase;
using Application.UseCases.ReservationCases.Commands.UpdateReservationCase;
using Application.UseCases.ReservationCases.Queries.GetReservationsByFilterCase;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Common.MappingProfiles;

public class ReservationMappingProfile : Profile
{
    public ReservationMappingProfile()
    {
        CreateMap<CreateReservationDto, CreateReservationCommand>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => Guid.Empty));
        
        CreateMap<CreateReservationCommand, Reservation>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ReceiptDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src =>DateTime.Now.AddDays(30)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ReservationStatuses.Confirmed));

        CreateMap<Reservation, ReadReservationDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ReceiptDate, opt => opt.MapFrom(src => src.ReceiptDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        
        CreateMap<DeleteReservationDto, DeleteReservationCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<UpdateReservationDto, UpdateReservationCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ReceiptDate, opt => opt.MapFrom(src => src.ReceiptDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        
        CreateMap<UpdateReservationCommand, Reservation>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ReceiptDate, opt => opt.MapFrom(src => src.ReceiptDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

        CreateMap<Reservation, ReadReservationReducedDto>() // TODO maybe remove this mapping
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.ReceiptDate, opt => opt.MapFrom(src => src.ReceiptDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        
        CreateMap<FilterReservationsDto, GetReservationsByFilterQuery>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.FromRecieptDate, opt => opt.MapFrom(src => src.FromRecieptDate))
            .ForMember(dest => dest.ToRecieptDate, opt => opt.MapFrom(src => src.ToRecieptDate))
            .ForMember(dest => dest.FromReturnDate, opt => opt.MapFrom(src => src.FromReturnDate))
            .ForMember(dest => dest.ToReturnDate, opt => opt.MapFrom(src => src.ToReturnDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.PageInfoDto, opt => opt.MapFrom(src => src.PageInfoDto));
    }
}