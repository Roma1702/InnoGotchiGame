namespace Core.Abstraction.Interfaces;

public interface IInnogotchiStateService
{
    public Task<bool> FeedAsync(string name);
    public Task<bool> DrinkAsync(string name);
    public Task IncreaseHungerLevelAsync();
    public Task IncreaseWaterLevelAsync();
    public Task IncreaseAgeAsync();
    public Task IncreaseHappinessDaysAsync();
}
