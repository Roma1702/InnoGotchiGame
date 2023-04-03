using AutoMapper;
using Entities.Identity;
using Models.Core;

namespace Mapping.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.ProfilePhoto, opt => opt.MapFrom(src => Convert.ToBase64String(src.ProfilePhoto!)))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName.Split(new char[] { ' ' })[0]))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.UserName.Split(new char[] { ' ' })[1]));
        CreateMap<UserDto, User>()
            .ForMember(dest => dest.ProfilePhoto, opt => opt.MapFrom(src => Convert.FromBase64String(src.ProfilePhoto!)))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"))
            .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email));
        CreateMap<ShortUserDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"))
            .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"))
            .ForMember(dest => dest.ProfilePhoto, opt => opt.MapFrom(src => Convert.FromBase64String(src.ProfilePhoto!)));
        CreateMap<User, ShortUserDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName.Split(new char[] { ' ' })[0]))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.UserName.Split(new char[] { ' ' })[1]))
            .ForMember(dest => dest.ProfilePhoto, opt => opt.MapFrom(src => Convert.ToBase64String(src.ProfilePhoto!)));
    }
}
