using Models.Core;

namespace DataAccessLayer.Abstraction.Interfaces;

public interface IFarmRepository
{
    public Task<List<FarmDto>> GetChunkAsync(int size, int number);
    public Task<FarmDto?> GetByIdAsync(Guid id);
    public Task CreateAsync(FarmDto farmDto);
    public Task UpdateAsync(FarmDto farmDto);
    public Task DeleteAsync(Guid id);
}
