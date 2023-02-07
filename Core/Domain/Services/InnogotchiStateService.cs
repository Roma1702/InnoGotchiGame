using Core.Abstraction.Interfaces;
using DataAccessLayer.Abstraction.Interfaces;

namespace Core.Domain.Services;

public class InnogotchiStateService : IInnogotchiStateService
{
    private readonly IInnogotchiStateRepository _stateRepository;

    public InnogotchiStateService(IInnogotchiStateRepository stateRepository)
    {
        _stateRepository = stateRepository;
    }

    public async Task DrinkAsync(string name)
    {
        await _stateRepository.DrinkAsync(name);
    }

    public async Task FeedAsync(string name)
    {
        await _stateRepository.FeedAsync(name);
    }

    public async Task IncreaseAgeAsync()
    {
        await _stateRepository.IncreaseAgeAsync();
    }

    public async Task IncreaseHungerLevelAsync()
    {
        await _stateRepository.IncreaseHungerLevelAsync();
    }

    public async Task IncreaseWaterLevelAsync()
    {
        await _stateRepository.IncreaseWaterLevelAsync();
    }

    public async Task IncreaseHappinessDaysAsync()
    {
        await _stateRepository.IncreaseHappinessDaysAsync();
    }
}
