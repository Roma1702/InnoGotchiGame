using Contracts.DTO;
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
    private readonly IUserRepository _userRepository;

    public FarmService(IFarmRepository farmRepository,
        IUserFriendRepository userFriendRepository,
        IValidator<FarmDto> validator,
        IUserRepository userRepository)
    {
        _farmRepository = farmRepository;
        _userFriendRepository = userFriendRepository;
        _validator = validator;
        _userRepository = userRepository;
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

    public async Task<IEnumerable<FarmDto>?> GetFarmsAsync(Guid id)
    {
        var userFriends = await _userFriendRepository.GetFriendsId(id);

        var farms = await _farmRepository.GetFarmsAsync(userFriends!);

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
        var userDto = await _userRepository.GetByIdAsync(userId);
        var user = await _userRepository.GetUserAsync(userDto!);

        if (user!.Farm is null || user!.Farm?.Pets?.Count == 0)
        {
            return new FarmStatisticDto();
        }

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
