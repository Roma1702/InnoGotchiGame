using Contracts.Entity;
using DataAccessLayer.Abstraction.Interfaces;
using DataAccessLayer.Data;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using static Contracts.Enum.Enums;

namespace DataAccessLayer.Repository;

public class InnogotchiStateRepository : IInnogotchiStateRepository
{
    private readonly ApplicationContext _context;
    private readonly DbSet<InnogotchiState> _dbSetState;
    private readonly DbSet<Innogotchi> _dbSetPets;
    private readonly DbSet<MealTime> _dbSetMeals;
    public InnogotchiStateRepository(ApplicationContext context)
    {
        _context = context;
        _dbSetState = context.Set<InnogotchiState>();
        _dbSetPets = context.Set<Innogotchi>();
        _dbSetMeals = context.Set<MealTime>();
    }

    public async Task DrinkAsync(string name)
    {
        var innogotchi = await _dbSetPets.FirstOrDefaultAsync(x => x.Name == name);

        if (innogotchi!.InnogotchiState!.Thirsty != ThirstyLevel.Full)
        {
            innogotchi!.InnogotchiState!.Thirsty -= 1;

            innogotchi!.InnogotchiState.StartOfHappinessDays = (innogotchi.InnogotchiState.Thirsty >= ThirstyLevel.Normal
                && innogotchi.InnogotchiState.Hunger >= HungerLevel.Normal)
                ? DateTimeOffset.Now : innogotchi!.InnogotchiState.StartOfHappinessDays;

            MealTime drinking = new()
            {
                InnogotchiStateId = innogotchi.InnogotchiState.Id,
                Time = DateTimeOffset.Now,
                MealType = MealType.Drinking,
            };

            await _dbSetMeals.AddAsync(drinking);
        }

        await _context.SaveChangesAsync();
    }

    public async Task FeedAsync(string name)
    {
        var innogotchi = await _dbSetPets.FirstOrDefaultAsync(x => x.Name == name);

        if (innogotchi!.InnogotchiState!.Hunger != HungerLevel.Full)
        {
            innogotchi!.InnogotchiState!.Hunger -= 1;

            innogotchi!.InnogotchiState.StartOfHappinessDays = (innogotchi.InnogotchiState.Thirsty >= ThirstyLevel.Normal
                && innogotchi.InnogotchiState.Hunger >= HungerLevel.Normal)
                ? DateTimeOffset.Now : innogotchi!.InnogotchiState.StartOfHappinessDays;

            MealTime feeding = new()
            {
                InnogotchiStateId = innogotchi.InnogotchiState.Id,
                Time = DateTimeOffset.Now,
                MealType = MealType.Feeding
            };

            await _dbSetMeals.AddAsync(feeding);
        }

        await _context.SaveChangesAsync();
    }

    public async Task IncreaseAgeAsync()
    {
        const int ageEquivalent = 7;

        var states = await _dbSetState.ToListAsync();

        foreach (var state in states)
        {
            if (state.Thirsty != ThirstyLevel.Dead && state.Hunger != HungerLevel.Dead)
            {
                state.Age = (DateTimeOffset.Now - state.Created).Days / ageEquivalent > state.Age
                    ? state.Age + 1 : state.Age;
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task IncreaseWaterLevelAsync()
    {
        const int thirstyPeriod = 2;

        var states = await _dbSetState.ToListAsync();

        foreach (var state in states)
        {
            if (state.Thirsty != ThirstyLevel.Dead && state.Hunger != HungerLevel.Dead)
            {
                state.Thirsty = (DateTimeOffset.Now - state.Created).Days / thirstyPeriod > (int)state.Thirsty
                    ? state.Thirsty + 1 : state.Thirsty;

                state.StartOfHappinessDays = state.Thirsty < ThirstyLevel.Normal
                    ? DateTimeOffset.Now : state.StartOfHappinessDays;
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task IncreaseHungerLevelAsync()
    {
        const int hungerPeriod = 3;

        var states = await _dbSetState.ToListAsync();

        foreach (var state in states)
        {
            if (state.Thirsty != ThirstyLevel.Dead && state.Hunger != HungerLevel.Dead)
            {
                state.Hunger = (DateTimeOffset.Now - state.Created).Days / hungerPeriod > (int)state.Hunger
                    ? state.Hunger + 1 : state.Hunger;

                state.StartOfHappinessDays = state.Hunger < HungerLevel.Normal
                    ? DateTimeOffset.Now : state.StartOfHappinessDays;
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task IncreaseHappinessDaysAsync()
    {
        var states = await _dbSetState.ToListAsync();

        foreach (var state in states)
        {
            state.HappinessDays = (state.Hunger >= HungerLevel.Normal && state.Thirsty >= ThirstyLevel.Normal)
                ? (DateTimeOffset.Now - state.StartOfHappinessDays).Days : 0;
        }

        await _context.SaveChangesAsync();
    }
}
