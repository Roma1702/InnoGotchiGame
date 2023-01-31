using Models.Core;

namespace DataAccessLayer.Abstraction.Interfaces;

public interface IInnogotchiPartRepository
{
    public Task CreateAsync(MediaDto mediaPartDto);
}