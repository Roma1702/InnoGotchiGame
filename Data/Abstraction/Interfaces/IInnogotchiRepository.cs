using Contracts.DTO;
using Entities.Entity;
using Models.Core;

namespace DataAccessLayer.Abstraction.Interfaces;

public interface IInnogotchiRepository
{
    public Task<List<PetInfoDto>?> GetChunkAsync(Guid farmId, int number, int size);
    public Task<List<PetInfoDto>?> SortByAgeAsync(Guid farmId, int number, int size);
    public Task<List<PetInfoDto>?> SortByHungerLevelAsync(Guid farmId, int number, int size);
    public Task<List<PetInfoDto>?> SortByWaterLevelAsync(Guid farmId, int number, int size);
    public Task<InnogotchiDto> GetByNameAsync(Guid farmId, string name);
    public Task CreateAsync(Farm farm, InnogotchiDto innogotchiDto);
    public Task UpdateAsync(InnogotchiDto innogotchiDto);
    public Task DeleteAsync(string name);
}
