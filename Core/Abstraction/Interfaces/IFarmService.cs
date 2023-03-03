using Contracts.DTO;
using Models.Core;

namespace Core.Abstraction.Interfaces;

public interface IFarmService
{
    public Task<IEnumerable<FarmDto>?> GetChunkAsync(Guid id, int number, int size);
    public Task<FarmDto?> GetByNameAsync(string name);
    public Task<FarmDto?> GetByIdAsync(Guid userId);
    public Task<FarmStatisticDto?> GetFarmStatistic(Guid userId);
    public Task CreateAsync(Guid userId, FarmDto farmDto);
    public Task UpdateAsync(Guid userId, FarmDto farmDto);
    public Task DeleteAsync(Guid userId);
}