using Models.Core;

namespace DataAccessLayer.Abstraction.Interfaces;

public interface IInnogotchiStateRepository
{
    public Task<InnogotchiStateDto?> GetByIdAsync(Guid id);
    public Task CreateAsync(InnogotchiDto innogotchiDto);
    public Task UpdateAsync(InnogotchiStateDto stateDto);
}
