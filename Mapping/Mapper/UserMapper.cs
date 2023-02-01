using AutoMapper;
using Entities.Entity;
using Entities.Identity;
using Models.Core;
namespace Mapping.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName.Split(new char[] { ' ' })[0]))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.UserName.Split(new char[] { ' ' })[1]))
            .ForMember(dest => dest.ProfilePhoto, opt => opt.Ignore());
        CreateMap<UserDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"))
            .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.ProfilePhoto, opt => opt.Ignore());
        CreateMap<ShortUserDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"))
            .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"))
            .ForMember(dest => dest.ProfilePhoto, opt => opt.Ignore())
            .ForMember(dest => dest.Farm, opt => opt.MapFrom(src => src.FarmDto));
        CreateMap<User, ShortUserDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName.Split(new char[] { ' ' })[0]))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.UserName.Split(new char[] { ' ' })[1]))
            .ForMember(dest => dest.ProfilePhoto, opt => opt.Ignore())
            .ForMember(dest => dest.FarmDto, opt => opt.MapFrom(src => src.Farm));
    }
}
