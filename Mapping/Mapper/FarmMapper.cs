using AutoMapper;
using Entities.Entity;
using Models.Core;

namespace Mapping.Mappers;

public class FarmMapper : Profile
{
    public FarmMapper()
    {
        CreateMap<Farm, FarmDto>().ReverseMap();
    }
}
