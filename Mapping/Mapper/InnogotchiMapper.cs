using AutoMapper;
using Entities.Entity;
using Models.Core;

namespace Mapping.Mappers;

public class InnogotchiMapper : Profile
{
	public InnogotchiMapper()
	{
		CreateMap<Innogotchi, InnogotchiDto>().ReverseMap();
	}
}
