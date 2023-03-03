using AutoMapper;
using Entities.Entity;
using Microsoft.AspNetCore.Http;
using Models.Core;

namespace Mapping.Mappers;

public class MediaMapper : Profile
{
    public MediaMapper()
    {
        CreateMap<InnogotchiPart, MediaDto>().ReverseMap();

        CreateMap<IFormFile, byte[]>()
            .ConvertUsing(new IFormFileToByteArray());

        CreateMap<byte[], IFormFile>()
            .ConvertUsing(new ByteArrayToIFromFile());
    }
    public class ByteArrayToIFromFile : ITypeConverter<byte[], IFormFile>
    {
        public IFormFile Convert(byte[] source, IFormFile destination, ResolutionContext context)
        {
            var stream = new MemoryStream(source!);

            IFormFile formFile = new FormFile(stream, 0, source!.Length, "name", "fileName");

            return formFile;
        }
    }
    public class IFormFileToByteArray : ITypeConverter<IFormFile, byte[]>
    {
        public byte[] Convert(IFormFile source, byte[] destination, ResolutionContext context)
        {
            byte[]? imageData = null;

            using (var binaryReader = new BinaryReader(source.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)source.Length);
            }

            return imageData ?? Array.Empty<byte>();
        }
    }
}
