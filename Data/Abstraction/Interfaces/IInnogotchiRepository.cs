using Contracts.DTO;
using Models.Core;

namespace DataAccessLayer.Abstraction.Interfaces;

public interface IInnogotchiRepository
{
    public Task<IEnumerable<PetInfoDto>?> GetChunkAsync(Guid farmId, int number, int size);
    public Task<IEnumerable<PetInfoDto>?> SortByAgeAsync(Guid farmId, int number, int size);
    public Task<IEnumerable<PetInfoDto>?> SortByHungerLevelAsync(Guid farmId, int number, int size);
    public Task<IEnumerable<PetInfoDto>?> SortByWaterLevelAsync(Guid farmId, int number, int size);
    public Task<InnogotchiDto> GetByNameAsync(Guid farmId, string name);
    public Task CreateAsync(Guid farmId, InnogotchiDto innogotchiDto);
    public Task UpdateAsync(InnogotchiDto innogotchiDto);
    public Task DeleteAsync(string name);
}
