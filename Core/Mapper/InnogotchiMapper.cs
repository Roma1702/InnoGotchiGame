using AutoMapper;
using Contracts.DTO;
using Entities.Entity;
using Models.Core;
using static Contracts.Enum.Enums;

namespace Mapping.Mappers;

public class InnogotchiMapper : Profile
{
	public InnogotchiMapper()
	{
        CreateMap<InnogotchiPart, MediaDto>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Convert.ToBase64String(src.Image!)));

        CreateMap<MediaDto, InnogotchiPart>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Convert.FromBase64String(src.Image!)))
            .ForMember(dest => dest.PartType, opt => opt.MapFrom(src => src.PartType));

        CreateMap<Innogotchi, InnogotchiDto>()
            .ForMember(dest => dest.Body, opt => opt
            .MapFrom(src => src.Parts!.FirstOrDefault(p => p.PartType == PartType.Body)))
            .ForMember(dest => dest.Eyes, opt => opt
            .MapFrom(src => src.Parts!.FirstOrDefault(p => p.PartType == PartType.Eyes)))
            .ForMember(dest => dest.Mouth, opt => opt
            .MapFrom(src => src.Parts!.FirstOrDefault(p => p.PartType == PartType.Mouth)))
            .ForMember(dest => dest.Nose, opt => opt
            .MapFrom(src => src.Parts!.FirstOrDefault(p => p.PartType == PartType.Nose)));

        CreateMap<InnogotchiDto, Innogotchi>()
            .ForMember(dest => dest.Parts, opt => opt.MapFrom(src => new List<InnogotchiPart>
            {
                new InnogotchiPart { Image = Convert.FromBase64String(src.Body!.Image!),
                    FileExtension = src.Body.FileExtension,
                    PartType = src.Body.PartType },
                new InnogotchiPart { Image = Convert.FromBase64String(src.Eyes!.Image!),
                    FileExtension = src.Eyes.FileExtension,
                    PartType = src.Eyes.PartType },
                new InnogotchiPart { Image = Convert.FromBase64String(src.Mouth!.Image!),
                    FileExtension = src.Mouth.FileExtension,
                    PartType = src.Mouth.PartType },
                new InnogotchiPart { Image = Convert.FromBase64String(src.Nose!.Image!),
                    FileExtension = src.Nose.FileExtension,
                    PartType = src.Nose.PartType },
            }));

        CreateMap<Innogotchi, PetInfoDto>()
			.ForMember(dest => dest.InnogotchiStateDto, opt => opt.MapFrom(src => src.InnogotchiState));
    }
}
