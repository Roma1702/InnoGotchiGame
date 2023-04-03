using AutoMapper;
using Contracts.DTO;
using DataAccessLayer.Abstraction.Interfaces;
using DataAccessLayer.Data;
using Entities.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models.Core;
using static Contracts.Enum.Enums;

namespace DataAccessLayer.Repository;

public class InnogotchiRepository : IInnogotchiRepository
{
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;
    private readonly DbSet<Innogotchi> _dbSetPets;
    private readonly DbSet<InnogotchiPart> _dbSetParts;
    private readonly DbSet<InnogotchiState> _dbSetState;
    public InnogotchiRepository(IMapper mapper,
        ApplicationContext context)
    {
        _mapper = mapper;
        _context = context;
        _dbSetPets = context.Set<Innogotchi>();
        _dbSetParts = context.Set<InnogotchiPart>();
        _dbSetState = context.Set<InnogotchiState>();
    }
    public async Task CreateAsync(Guid farmId, InnogotchiDto innogotchiDto)
    {
        var innogotchi = _mapper.Map<Innogotchi>(innogotchiDto);

        innogotchi.FarmId = farmId;

        using var transaction = _context.Database.BeginTransaction();

        try
        {
            await _dbSetPets.AddAsync(innogotchi);

            //await AddPartsAsync(innogotchiDto, innogotchi);

            await AddStateAsync(innogotchi);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }
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

        return innogotchiDto;
    }

    public async Task<PetInfoDto?> GetStateByNameAsync(Guid farmId, string name)
    {
        var pets = await _dbSetPets.AsNoTracking()
            .Include(x => x.InnogotchiState)
            .Include(x => x.Parts)
            .Where(x => x.FarmId == farmId)
            .FirstOrDefaultAsync(x => x.Name == name);

        if (pets is null) return null;

        var petDto = _mapper.Map<PetInfoDto>(pets);

        return petDto;
    }

    public async Task<int> GetCountAsync(Guid farmId)
    {
        var count = _dbSetPets
            .AsNoTracking()
            .Include(x => x.Farm)
            .Count(x => x.FarmId == farmId);

        return await Task.FromResult(count);
    }

    public async Task<IEnumerable<InnogotchiDto>?> GetChunkAsync(Guid farmId, int number, int size)
    {
        var pets = await _dbSetPets.AsNoTracking()
            .Include(x => x.InnogotchiState)
            .Include(x => x.Parts)
            .OrderBy(x => x.InnogotchiState!.HappinessDays)
            .Where(x => x.FarmId == farmId)
            .Skip(number * size)
            .Take(size)
            .ToListAsync();

        if (pets is null) return null;

        var petsDto = _mapper.Map<List<InnogotchiDto>>(pets);

        return petsDto;
    }

    public async Task<IEnumerable<PetInfoDto>?> SortByHappinessDays(Guid farmId, int number, int size)
    {
        var pets = await _dbSetPets.AsNoTracking()
            .Include(x => x.InnogotchiState)
            .Include(x => x.Parts)
            .Where(x => x.FarmId == farmId)
            .OrderBy(x => x.InnogotchiState!.HappinessDays)
            .Skip(number * size)
            .Take(size)
            .ToListAsync();

        if (pets is null) return null;

        var petsDto = _mapper.Map<List<PetInfoDto>>(pets);

        return petsDto;
    }

    public async Task<IEnumerable<PetInfoDto>?> SortByAgeAsync(Guid farmId, int number, int size)
    {
        var pets = await _dbSetPets.AsNoTracking()
            .Include(x => x.InnogotchiState)
            .Include(x => x.Parts)
            .Where(x => x.FarmId == farmId)
            .OrderBy(x => x.InnogotchiState!.Age)
            .Skip(number * size)
            .Take(size)
            .ToListAsync();

        if (pets is null) return null;

        var petsDto = _mapper.Map<List<PetInfoDto>>(pets);

        return petsDto;
    }
    public async Task<IEnumerable<PetInfoDto>?> SortByHungerLevelAsync(Guid farmId, int number, int size)
    {
        var pets = await _dbSetPets.AsNoTracking()
            .Include(x => x.InnogotchiState)
            .Include(x => x.Parts)
            .Where(x => x.FarmId == farmId)
            .OrderByDescending(x => x.InnogotchiState!.Hunger)
            .Skip(number * size)
            .Take(size)
            .ToListAsync();

        if (pets is null) return null;

        var petsDto = _mapper.Map<List<PetInfoDto>>(pets);

        return petsDto;
    }
    public async Task<IEnumerable<PetInfoDto>?> SortByWaterLevelAsync(Guid farmId, int number, int size)
    {
        var pets = await _dbSetPets.AsNoTracking()
            .Include(x => x.InnogotchiState)
            .Include(x => x.Parts)
            .Where(x => x.FarmId == farmId)
            .OrderByDescending(x => x.InnogotchiState!.Thirsty)
            .Skip(number * size)
            .Take(size)
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

    private async Task AddStateAsync(Innogotchi innogotchi)
    {
        InnogotchiState state = new()
        {
            Age = 0,
            Hunger = HungerLevel.Normal,
            Thirsty = ThirstyLevel.Normal,
            StartOfHappinessDays = DateTimeOffset.Now,
            HappinessDays = 0,
            Created = DateTimeOffset.Now,
            Innogotchi = innogotchi
        };

        await _dbSetState.AddAsync(state);
    }
}
