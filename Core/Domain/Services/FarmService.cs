﻿using Contracts.DTO;
using Core.Abstraction.Interfaces;
using DataAccessLayer.Abstraction.Interfaces;
using FluentValidation;
using Models.Core;

namespace Core.Domain.Services;

public class FarmService : IFarmService
{
    private readonly IFarmRepository _farmRepository;
    private readonly IUserFriendRepository _userFriendRepository;
    private readonly IValidator<FarmDto> _validator;

    public FarmService(IFarmRepository farmRepository,
        IUserFriendRepository userFriendRepository,
        IValidator<FarmDto> validator)
    {
        _farmRepository = farmRepository;
        _userFriendRepository = userFriendRepository;
        _validator = validator;
    }
    public async Task CreateAsync(Guid userId, FarmDto farmDto)
    {
        var farm = _validator.Validate(farmDto);

        if (farm.IsValid)
        {
            await _farmRepository.CreateAsync(userId, farmDto);
        }
    }

    public async Task DeleteAsync(Guid userId)
    {
        await _farmRepository.DeleteAsync(userId);
    }

    public async Task<FarmDto?> GetByNameAsync(string name)
    {
        var farm = await _farmRepository.GetByNameAsync(name);

        return farm ?? null;
    }

    public async Task<FarmDto?> GetByIdAsync(Guid userId)
    {
        var farm = await _farmRepository.GetByIdAsync(userId);

        return farm ?? null;
    }

    public async Task<IEnumerable<FarmDto>?> GetChunkAsync(Guid id, int number, int size)
    {
        var userFriends = await _userFriendRepository.GetFriendsId(id);

        var farms = await _farmRepository.GetChunkAsync(userFriends!, number, size);

        return farms ?? null;
    }

    public async Task UpdateAsync(Guid userId, FarmDto farmDto)
    {
        var farm = _validator.Validate(farmDto);

        if (farm.IsValid)
        {
            await _farmRepository.UpdateAsync(userId, farmDto);
        }
    }

    public async Task<FarmStatisticDto?> GetFarmStatistic(Guid userId)
    {
        var farmStatistic = await HandleFarmStatistic(userId);

        return farmStatistic;
    }

    private async Task<FarmStatisticDto> HandleFarmStatistic(Guid userId)
    {
        var countOfAlivePets = await _farmRepository.GetCountOfAliveAsync(userId);
        var countOfDeadPets = await _farmRepository.GetCountOfDeadAsync(userId);
        var averageFeedPeriod = await _farmRepository.GetAverageFeedPeriodAsync(userId);
        var averageDrinking = await _farmRepository.GetAverageDrinkPeriodAsync(userId);
        var averageHappinessDays = await _farmRepository.GetAverageHappinessDaysCount(userId);
        var averageAge = await _farmRepository.GetAverageAgeAsync(userId);

        FarmStatisticDto farmStatistic = new()
        {
            CountOfAlivePets = countOfAlivePets,
            CountOfDeadPets = countOfDeadPets,
            AverageFeedPeriod = averageFeedPeriod,
            AverageDrinking = averageDrinking,
            AverageHappinessDays = averageHappinessDays,
            AverageAge = averageAge
        };

        return farmStatistic;
    }
}
