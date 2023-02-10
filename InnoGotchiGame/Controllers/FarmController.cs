using Contracts.DTO;
using Core.Abstraction.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Core;

namespace InnoGotchiGame.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FarmController : ControllerBase
{
    private readonly IFarmService _farmService;
    private readonly IIdentityService _identityService;

    public FarmController(IFarmService farmService,
        IIdentityService identityService)
	{
        _farmService = farmService;
        _identityService = identityService;
    }

    [Authorize]
    [HttpGet("friendsFarms")]
    public async Task<List<FarmDto>?> GetChunkAsync(int number, int size)
    {
        var userId = _identityService.GetUserIdentity();

        return await _farmService.GetChunkAsync(Guid.Parse(userId), number, size);
    }

    [Authorize]
    [HttpGet]
    public async Task<FarmDto?> GetByIdAsync()
    {
        var userId = _identityService.GetUserIdentity();

        return await _farmService.GetByIdAsync(Guid.Parse(userId));
    }

    [Authorize]
    [HttpGet("statistic")]
    public async Task<FarmStatisticDto?> GetFarmStatistic()
    {
        var userId = _identityService.GetUserIdentity();

        return await _farmService.GetFarmStatistic(Guid.Parse(userId));
    }

    [Authorize]
    [HttpGet("name")]
    public async Task<FarmDto?> GetByNameAsync(string name)
    {
        return await _farmService.GetByNameAsync(name);
    }

    [Authorize]
    [HttpPost("create")]
    public async Task CreateAsync(FarmDto farmDto)
    {
        var userId = _identityService.GetUserIdentity();

        if (userId != string.Empty)
        {
            await _farmService.CreateAsync(Guid.Parse(userId), farmDto);
        }
    }

    [Authorize]
    [HttpPut("edit")]
    public async Task UpdateAsync(FarmDto farmDto)
    {
        var userId = _identityService.GetUserIdentity();

        if (userId != string.Empty)
        {
            await _farmService.UpdateAsync(Guid.Parse(userId), farmDto);
        }
    }

    [Authorize]
    [HttpDelete("delete")]
    public async Task DeleteAsync()
    {
        var userId = _identityService.GetUserIdentity();

        if (userId != string.Empty)
        {
            await _farmService.DeleteAsync(Guid.Parse(userId));
        }
    }
}
