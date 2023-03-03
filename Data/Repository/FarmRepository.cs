using AutoMapper;
using DataAccessLayer.Abstraction.Interfaces;
using DataAccessLayer.Data;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Models.Core;
using static Contracts.Enum.Enums;

namespace DataAccessLayer.Repository;

public class FarmRepository : IFarmRepository
{
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;
    private readonly DbSet<Farm> _dbSetFarms;
    public FarmRepository(IMapper mapper,
            ApplicationContext context)
    {
        _mapper = mapper;
        _context = context;
        _dbSetFarms = context.Set<Farm>();
    }
    public async Task CreateAsync(Guid userId, FarmDto farmDto)
    {
        var farm = _mapper.Map<Farm>(farmDto);

        farm.UserId = userId;

        await _dbSetFarms.AddAsync(farm);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid userId)
    {
        var farm = await _dbSetFarms.FirstOrDefaultAsync(x => x.UserId == userId);

        if (farm is not null)
        {
            _dbSetFarms.Remove(farm);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<FarmDto?> GetByNameAsync(string name)
    {
        var farm = await _dbSetFarms.AsNoTracking()
            .Include(x => x.Pets)
            .FirstOrDefaultAsync(x => x.Name == name);

        if (farm is null) return null;

        var farmDto = _mapper.Map<FarmDto>(farm);

        return farmDto;
    }

    public async Task<FarmDto?> GetByIdAsync(Guid userId)
    {
        var farm = await _dbSetFarms.AsNoTracking()
            .Include(x => x.Pets)
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (farm is null) return null;

        var farmDto = _mapper.Map<FarmDto>(farm);

        return farmDto;
    }

    public async Task<IEnumerable<FarmDto>?> GetChunkAsync(IEnumerable<Guid> userFriends, int number, int size)
    {
        var farms = await _dbSetFarms.Where(x => userFriends.Any(c => c == x.UserId))
            .Skip(number * size)
            .Take(size)
            .ToListAsync();

        if (farms is null) return null;

        var farmsDto = _mapper.Map<List<FarmDto>>(farms);

        return farmsDto;
    }

    public async Task UpdateAsync(Guid userId, FarmDto farmDto)
    {
        var farm = await _dbSetFarms.FirstOrDefaultAsync(x => x.UserId == userId);

        if (farm is not null)
        {
            var mapFarm = _mapper.Map<Farm>(farmDto);

            farm!.Name = mapFarm.Name;

            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetCountOfAliveAsync(Guid userId)
    {
        var ownFarm = await GetFarmAsync(userId);

        var count = ownFarm!.Pets!.Count(x => x.InnogotchiState!.Hunger != HungerLevel.Dead
            && x.InnogotchiState.Thirsty != ThirstyLevel.Dead);

        return count;
    }

    public async Task<int> GetCountOfDeadAsync(Guid userId)
    {
        var ownFarm = await GetFarmAsync(userId);

        var count = ownFarm!.Pets!.Count(x => x.InnogotchiState!.Hunger == HungerLevel.Dead
            && x.InnogotchiState.Thirsty == ThirstyLevel.Dead);

        return count;
    }

    public async Task<double> GetAverageFeedPeriodAsync(Guid userId)
    {
        var ownFarm = await GetFarmAsync(userId);

        var pets = ownFarm!.Pets!;

        var feedingDates = GetAverageMealPeriodAsync(pets, MealType.Feeding);

        var allAvgPeriod = feedingDates.Count() > 0 ? feedingDates.Average() : 0;

        return allAvgPeriod;
    }

    public async Task<double> GetAverageDrinkPeriodAsync(Guid userId)
    {
        var ownFarm = await GetFarmAsync(userId);

        var pets = ownFarm!.Pets!;

        var feedingDates = GetAverageMealPeriodAsync(pets, MealType.Drinking);

        var allAvgPeriod = feedingDates.Count() > 0 ? feedingDates.Average() : 0;

        return allAvgPeriod;
    }

    public async Task<double> GetAverageHappinessDaysCount(Guid userId)
    {
        var ownFarm = await GetFarmAsync(userId);

        var avgHappyDays = ownFarm!.Pets!.Average(x => x.InnogotchiState!.HappinessDays);

        return Math.Round(avgHappyDays, 1);
    }

    public async Task<double> GetAverageAgeAsync(Guid userId)
    {
        var ownFarm = await GetFarmAsync(userId);

        var avgHappyDays = ownFarm!.Pets!.Average(x => x.InnogotchiState!.Age);

        return Math.Round(avgHappyDays, 1);
    }

    private async Task<Farm?> GetFarmAsync(Guid userId)
    {
        var ownFarm = await _dbSetFarms.AsNoTracking()
            .Include(x => x.Pets!)
            .ThenInclude(x => x.InnogotchiState!)
            .ThenInclude(x => x.MealTimes)
            .FirstOrDefaultAsync(x => x.UserId == userId);

        return ownFarm;
    }

    private IEnumerable<double> GetAverageMealPeriodAsync(IEnumerable<Innogotchi> pets, MealType mealType)
    {
        List<double> feedingDates = new();

        foreach (var item in pets)
        {
            double avgPeriod = 0;

            var mealTime = item.InnogotchiState!.MealTimes?
                .Where(x => x.MealType == mealType)
                .Select(x => x.Time)
                .ToList();

            if (mealTime?.Count() > 0)
            {
                avgPeriod = Enumerable.Range(1, mealTime.Count() - 1)
                    .Select(x => (mealTime[x] - mealTime[x - 1]).TotalDays)
                    .Average();
            }

            feedingDates.Add(avgPeriod);
        }

        return feedingDates;
    }
}
