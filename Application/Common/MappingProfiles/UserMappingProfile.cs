using Application.Common.Dtos.User;
using Application.UseCases.UserCases.Commands.AuthenticationUserCase;
using Application.UseCases.UserCases.Commands.DeleteUserCase;
using Application.UseCases.UserCases.Commands.RegisterUserCase;
using Application.UseCases.UserCases.Commands.UpdateUserCase;
using Application.UseCases.UserCases.Commands.UpdateUserRoleCase;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Common.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegisterUserDto, RegisterUserCommand>()
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage));
        
        CreateMap<RegisterUserCommand, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.RefreshTokenId, opt => opt.Ignore())
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.ProfileImage, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Roles.User));
        
        CreateMap<User, ReadUserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage));
        
        CreateMap<AuthUserDto, AuthenticationUserCommand>()
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        
        CreateMap<UpdateUserDto, UpdateUserCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Empty))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage));
        
        CreateMap<UpdateUserCommand, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.RefreshTokenId, opt => opt.Ignore())
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.ProfileImage, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore());
        
        CreateMap<DeleteUserDto, DeleteUserCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        
        CreateMap<UpdateUserRoleDto, UpdateUserRoleCommand>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        
        CreateMap<User, ReadUserRoleDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

        CreateMap<User, ReadUserReducedDto>() // TODO maybe remove this mapping
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName));
    }
}