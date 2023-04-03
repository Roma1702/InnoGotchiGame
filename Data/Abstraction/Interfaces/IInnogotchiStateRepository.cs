namespace DataAccessLayer.Abstraction.Interfaces;

public interface IInnogotchiStateRepository
{
    public Task<bool> FeedAsync(string name);
    public Task<bool> DrinkAsync(string name);
    public Task IncreaseAgeAsync();
    public Task IncreaseHungerLevelAsync();
    public Task IncreaseWaterLevelAsync();
    public Task IncreaseHappinessDaysAsync();
}
