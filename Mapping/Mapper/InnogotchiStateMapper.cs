using AutoMapper;
using Entities.Entity;
using Models.Core;

namespace Mapping.Mappers;

public class InnogotchiStateMapper : Profile
{
	public InnogotchiStateMapper()
	{
		CreateMap<InnogotchiState, InnogotchiStateDto>().ReverseMap();
	}
}
