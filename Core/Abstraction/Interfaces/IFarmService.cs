using Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Models.Core;

namespace Core.Abstraction.Interfaces;

public interface IFarmService
{
    public Task<List<FarmDto>?> GetChunkAsync(Guid id, int number, int size);
    public Task<FarmDto?> GetByNameAsync(string name);
    public Task<FarmDto?> GetByIdAsync(Guid userId);
    public Task<FarmStatisticDto?> GetFarmStatistic(Guid userId);
    public Task<IActionResult> CreateAsync(Guid userId, FarmDto farmDto);
    public Task<IActionResult> UpdateAsync(Guid userId, FarmDto farmDto);
    public Task<IActionResult> DeleteAsync(Guid userId);
}