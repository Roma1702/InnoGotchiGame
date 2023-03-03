namespace DataAccessLayer.Abstraction.Interfaces;

public interface IInnogotchiStateRepository
{
    public Task FeedAsync(string name);
    public Task DrinkAsync(string name);
    public Task IncreaseAgeAsync();
    public Task IncreaseHungerLevelAsync();
    public Task IncreaseWaterLevelAsync();
    public Task IncreaseHappinessDaysAsync();
}
