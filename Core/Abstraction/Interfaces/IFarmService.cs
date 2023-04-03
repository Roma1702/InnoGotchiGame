using Contracts.DTO;
using Models.Core;

namespace Core.Abstraction.Interfaces;

public interface IFarmService
{
    public Task<IEnumerable<FarmDto>?> GetFarmsAsync(Guid id);
    public Task<FarmDto?> GetByNameAsync(string name);
    public Task<FarmDto?> GetByIdAsync(Guid userId);
    public Task<FarmStatisticDto?> GetFarmStatistic(Guid userId);
    public Task CreateAsync(Guid userId, FarmDto farmDto);
    public Task UpdateAsync(Guid userId, FarmDto farmDto);
    public Task DeleteAsync(Guid userId);
}