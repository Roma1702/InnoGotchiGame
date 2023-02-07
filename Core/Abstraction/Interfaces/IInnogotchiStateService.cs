namespace Core.Abstraction.Interfaces;

public interface IInnogotchiStateService
{
    public Task FeedAsync(string name);
    public Task DrinkAsync(string name);
    public Task IncreaseHungerLevelAsync();
    public Task IncreaseWaterLevelAsync();
    public Task IncreaseAgeAsync();
    public Task IncreaseHappinessDaysAsync();
}
