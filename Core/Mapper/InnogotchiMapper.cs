using AutoMapper;
using Contracts.DTO;
using Entities.Entity;
using Models.Core;

namespace Mapping.Mappers;

public class InnogotchiMapper : Profile
{
	public InnogotchiMapper()
	{
		CreateMap<Innogotchi, InnogotchiDto>().ReverseMap();
		CreateMap<MediaDto, InnogotchiPart>()
			.ForMember(dest => dest.Image, opt => opt.Ignore());
        CreateMap<InnogotchiPart, MediaDto>()
            .ForMember(dest => dest.Image, opt => opt.Ignore());
		CreateMap<Innogotchi, PetInfoDto>()
			.ForMember(dest => dest.InnogotchiStateDto, opt => opt.MapFrom(src => src.InnogotchiState));
    }
}
