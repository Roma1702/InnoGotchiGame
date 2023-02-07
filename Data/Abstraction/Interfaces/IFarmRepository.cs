using Entities.Identity;
using Models.Core;

namespace DataAccessLayer.Abstraction.Interfaces;

public interface IFarmRepository
{
    public Task<int> GetCountOfAliveAsync(Guid userId);
    public Task<int> GetCountOfDeadAsync(Guid userId);
    public Task<double> GetAverageFeedPeriodAsync(Guid userId);
    public Task<double> GetAverageDrinkPeriodAsync(Guid userId);
    public Task<double> GetAverageHappinessDaysCount(Guid userId);
    public Task<double> GetAverageAgeAsync(Guid userId);
    public Task<List<FarmDto>?> GetChunkAsync(List<Guid> userFriends, int number, int size);
    public Task<FarmDto?> GetByNameAsync(string name);
    public Task<FarmDto?> GetByIdAsync(Guid userId);
    public Task CreateAsync(Guid userId, FarmDto farmDto);
    public Task UpdateAsync(Guid userId, FarmDto farmDto);
    public Task DeleteAsync(Guid userId);
}