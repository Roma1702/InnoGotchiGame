using Models.Core;

namespace DataAccessLayer.Abstraction.Interfaces;

public interface IInnogotchiRepository
{
    public Task<List<InnogotchiDto>> GetChunkAsync(int number, int size);
    public Task<InnogotchiDto> GetById(Guid id);
    public Task CreateAsync(InnogotchiDto innogotchiDto);
    public Task UpdateAsync(InnogotchiDto innogotchiDto);
    public Task DeleteAsync(Guid id);
}
