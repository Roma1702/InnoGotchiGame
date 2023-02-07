using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Models.Core;

namespace Core.Abstraction.Interfaces;

public interface IInnogotchiService
{
    public Task<List<PetInfoDto>?> GetChunkAsync(Guid userId, int number, int size);
    public Task<List<PetInfoDto>?> SortByAgeAsync(Guid userId, int number, int size);
    public Task<List<PetInfoDto>?> SortByHungerLevelAsync(Guid userId, int number, int size);
    public Task<List<PetInfoDto>?> SortByWaterLevelAsync(Guid userId, int number, int size);
    public Task<InnogotchiDto?> GetByNameAsync(Guid userId, string name);
    public Task<IActionResult> CreateAsync(Guid userId, InnogotchiDto dto);
    public Task<IActionResult> UpdateAsync(InnogotchiDto dto);
    public Task<IActionResult> DeleteAsync(string name);
}
