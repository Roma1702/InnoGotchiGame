using Contracts.DTO;
using Models.Core;

namespace Core.Abstraction.Interfaces;

public interface IInnogotchiService
{
    public Task<int> GetCountAsync(Guid userId);
    public Task<IEnumerable<InnogotchiDto>?> GetChunkAsync(Guid userId, int number, int size);
    public Task<IEnumerable<PetInfoDto>?> SortByHappinessDays(Guid farmId, int number, int size);
    public Task<IEnumerable<PetInfoDto>?> SortByAgeAsync(Guid userId, int number, int size);
    public Task<IEnumerable<PetInfoDto>?> SortByHungerLevelAsync(Guid userId, int number, int size);
    public Task<IEnumerable<PetInfoDto>?> SortByWaterLevelAsync(Guid userId, int number, int size);
    public Task<InnogotchiDto?> GetByNameAsync(Guid userId, string name);
    public Task<PetInfoDto?> GetStateByNameAsync(Guid userId, string name);
    public Task CreateAsync(Guid userId, InnogotchiDto dto);
    public Task UpdateAsync(InnogotchiDto dto);
    public Task DeleteAsync(string name);
}
