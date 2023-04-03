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
    public async Task<IEnumerable<FarmDto>?> GetFarmsAsync()
    {
        var userId = _identityService.GetUserIdentity();

        return await _farmService.GetFarmsAsync(Guid.Parse(userId));
    }

    [Authorize]
    [HttpGet]
    public async Task<FarmDto?> GetOwnFarmAsync()
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
    [HttpGet("{name}")]
    public async Task<FarmDto?> GetByNameAsync(string name)
    {
        return await _farmService.GetByNameAsync(name);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] FarmDto farmDto)
    {
        var userId = _identityService.GetUserIdentity();

        if (userId != string.Empty)
        {
            await _farmService.CreateAsync(Guid.Parse(userId), farmDto);

            return Ok(farmDto);
        }

        return BadRequest();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromForm] FarmDto farmDto)
    {
        var userId = _identityService.GetUserIdentity();

        if (userId != string.Empty)
        {
            await _farmService.UpdateAsync(Guid.Parse(userId), farmDto);

            return Ok(farmDto);
        }

        return BadRequest();
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync()
    {
        var userId = _identityService.GetUserIdentity();

        if (userId != string.Empty)
        {
            await _farmService.DeleteAsync(Guid.Parse(userId));

            return Ok(userId);
        }

        return NotFound();
    }
}
