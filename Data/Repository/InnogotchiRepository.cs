using AutoMapper;
using Contracts.DTO;
using DataAccessLayer.Abstraction.Interfaces;
using DataAccessLayer.Data;
using Entities.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models.Core;
using System.Net.Mime;
using static Contracts.Enum.Enums;

namespace DataAccessLayer.Repository;

public class InnogotchiRepository : IInnogotchiRepository
{
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;
    private readonly DbSet<Innogotchi> _dbSetPets;
    private readonly DbSet<InnogotchiPart> _dbSetParts;
    public InnogotchiRepository(IMapper mapper,
        ApplicationContext context)
    {
        _mapper = mapper;
        _context = context;
        _dbSetPets = context.Set<Innogotchi>();
        _dbSetParts = context.Set<InnogotchiPart>();
    }
    public async Task CreateAsync(Guid farmId, InnogotchiDto innogotchiDto)
    {
        var innogotchi = _mapper.Map<Innogotchi>(innogotchiDto);

        innogotchi.FarmId = farmId;

        await _dbSetPets.AddAsync(innogotchi);

        await AddPartsAsync(innogotchiDto, innogotchi);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string name)
    {
        var innogotchi = await _dbSetPets.FirstOrDefaultAsync(x => x.Name == name);

        if (innogotchi is not null)
        {
            _dbSetPets.Remove(innogotchi);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<InnogotchiDto> GetByNameAsync(Guid farmId, string name)
    {
        var innogotchi = await _dbSetPets.AsNoTracking()
            .Include(x => x.Farm)
            .Include(x => x.InnogotchiState)
            .Include(x => x.Parts)
            .Where(x => x.FarmId == farmId)
            .FirstOrDefaultAsync(x => x.Name == name);

        var innogotchiDto = _mapper.Map<InnogotchiDto>(innogotchi);

        ConvertToFormFile(innogotchiDto, innogotchi!);

        return innogotchiDto;
    }

    public async Task<List<PetInfoDto>?> GetChunkAsync(Guid farmId, int number, int size)
    {
        var pets = await _dbSetPets.AsNoTracking()
            .Include(x => x.InnogotchiState)
            .Include(x => x.Parts)
            .Where(x => x.FarmId == farmId)
            .Skip(number * size)
            .Take(size)
            .OrderBy(x => x.InnogotchiState!.HappinessDays)
            .ToListAsync();

        if (pets is null) return null;

        var petsDto = _mapper.Map<List<PetInfoDto>>(pets);

        return petsDto;
    }
    public async Task<List<PetInfoDto>?> SortByAgeAsync(Guid farmId, int number, int size)
    {
        var pets = await _dbSetPets.AsNoTracking()
            .Include(x => x.InnogotchiState)
            .Include(x => x.Parts)
            .Where(x => x.FarmId == farmId)
            .Skip(number * size)
            .Take(size)
            .OrderBy(x => x.InnogotchiState!.Age)
            .ToListAsync();

        if (pets is null) return null;

        var petsDto = _mapper.Map<List<PetInfoDto>>(pets);

        return petsDto;
    }
    public async Task<List<PetInfoDto>?> SortByHungerLevelAsync(Guid farmId, int number, int size)
    {
        var pets = await _dbSetPets.AsNoTracking()
            .Include(x => x.InnogotchiState)
            .Include(x => x.Parts)
            .Where(x => x.FarmId == farmId)
            .Skip(number * size)
            .Take(size)
            .OrderBy(x => x.InnogotchiState!.Hunger)
            .ToListAsync();

        if (pets is null) return null;

        var petsDto = _mapper.Map<List<PetInfoDto>>(pets);

        return petsDto;
    }
    public async Task<List<PetInfoDto>?> SortByWaterLevelAsync(Guid farmId, int number, int size)
    {
        var pets = await _dbSetPets.AsNoTracking()
            .Include(x => x.InnogotchiState)
            .Include(x => x.Parts)
            .Where(x => x.FarmId == farmId)
            .Skip(number * size)
            .Take(size)
            .OrderBy(x => x.InnogotchiState!.Thirsty)
            .ToListAsync();

        if (pets is null) return null;

        var petsDto = _mapper.Map<List<PetInfoDto>>(pets);

        return petsDto;
    }

    public async Task UpdateAsync(InnogotchiDto innogotchiDto)
    {
        var innogotchi = _mapper.Map<Innogotchi>(innogotchiDto);

        _context.Entry(innogotchi).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }

    private byte[] ConvertToByteArray(IFormFile image)
    {
        using var binaryReader = new BinaryReader(image.OpenReadStream());

        byte[]? imageData = binaryReader.ReadBytes((int)image.Length);

        return imageData;
    }

    private async Task AddPartsAsync(InnogotchiDto innogotchiDto, Innogotchi innogotchi)
    {
        InnogotchiPart body = new()
        {
            Image = ConvertToByteArray(innogotchiDto.Body!.Image!),
            PartType = PartType.Body,
            InnogotchiId = innogotchi.Id
        };
        InnogotchiPart nose = new()
        {
            Image = ConvertToByteArray(innogotchiDto.Body!.Image!),
            PartType = PartType.Nose,
            InnogotchiId = innogotchi.Id
        };
        InnogotchiPart eyes = new()
        {
            Image = ConvertToByteArray(innogotchiDto.Body!.Image!),
            PartType = PartType.Eyes,
            InnogotchiId = innogotchi.Id
        };
        InnogotchiPart mouth = new()
        {
            Image = ConvertToByteArray(innogotchiDto.Body!.Image!),
            PartType = PartType.Mouth,
            InnogotchiId = innogotchi.Id
        };

        await _dbSetParts.AddRangeAsync(body, mouth, nose, eyes);
    }

    private void ConvertToFormFile(InnogotchiDto innogotchiDto, Innogotchi innogotchi)
    {
        var partDto = new MediaDto();

        foreach (var part in innogotchi!.Parts!)
        {
            using (var stream = new MemoryStream(part!.Image!))
            {
                var formFile = new FormFile(stream, 0, part!.Image!.Length, "photo", "fileName")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/json"
                };

                ContentDisposition cd = new()
                {
                    FileName = formFile.FileName
                };
                formFile.ContentDisposition = cd.ToString();

                if (part.PartType == PartType.Body)
                {
                    innogotchiDto.Body = partDto;
                    innogotchiDto.Body!.Image = formFile;
                }
                else if (part.PartType == PartType.Nose)
                {
                    innogotchiDto.Nose = partDto;
                    innogotchiDto.Nose!.Image = formFile;
                }
                else if (part.PartType == PartType.Mouth)
                {
                    innogotchiDto.Mouth = partDto;
                    innogotchiDto.Mouth!.Image = formFile;
                }
                else if (part.PartType == PartType.Eyes)
                {
                    innogotchiDto.Eyes = partDto;
                    innogotchiDto.Eyes!.Image = formFile;
                }
            };
        }
    }
}
