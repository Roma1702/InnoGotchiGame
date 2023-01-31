using Entities.Identity;
using Models.Core;

namespace DataAccessLayer.Abstraction.Interfaces;

public interface IFarmRepository
{
    public Task<List<FarmDto>?> GetChunkAsync(Guid id, int number, int size);
    public Task<FarmDto?> GetByNameAsync(string name);
    public Task<FarmDto?> GetByIdAsync(Guid id);
    public Task CreateAsync(User user, FarmDto farmDto);
    public Task UpdateAsync(User user, FarmDto farmDto);
    public Task DeleteAsync(User user);
}