using Entities.Identity;
using Models.Core;

namespace DataAccessLayer.Abstraction.Interfaces;

public interface IFarmRepository
{
    public Task<int> GetCountOfAliveAsync(Guid id);
    public Task<int> GetCountOfDeadAsync(Guid id);
    public Task<double> GetAverageFeedPeriodAsync(Guid id);
    public Task<double> GetAverageDrinkPeriodAsync(Guid id);
    public Task<double> GetAverageHappinessDaysCount(Guid id);
    public Task<double> GetAverageAgeAsync(Guid id);
    public Task<List<FarmDto>?> GetChunkAsync(List<Guid> userFriends, int number, int size);
    public Task<FarmDto?> GetByNameAsync(string name);
    public Task<FarmDto?> GetByIdAsync(Guid id);
    public Task CreateAsync(User user, FarmDto farmDto);
    public Task UpdateAsync(User user, FarmDto farmDto);
    public Task DeleteAsync(User user);
}