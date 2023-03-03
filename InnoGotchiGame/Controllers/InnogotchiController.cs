using Contracts.DTO;
using Core.Abstraction.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Core;

namespace InnoGotchiGame.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InnogotchiController : ControllerBase
{
    private readonly IInnogotchiService _innogotchiService;
    private readonly IIdentityService _identityService;
    private readonly IInnogotchiStateService _innogotchiStateService;

    public InnogotchiController(IInnogotchiService innogotchiService,
        IIdentityService identityService,
        IInnogotchiStateService innogotchiStateService)
    {
        _innogotchiService = innogotchiService;
        _identityService = identityService;
        _innogotchiStateService = innogotchiStateService;
    }

    [Authorize]
    [HttpGet("chunk")]
    public async Task<IEnumerable<PetInfoDto>?> GetChunkAsync(int number = 0, int size = 15)
    {
        var userId = _identityService.GetUserIdentity();

        return await _innogotchiService.GetChunkAsync(Guid.Parse(userId), number, size);
    }

    [Authorize]
    [HttpGet("sortByAge")]
    public async Task<IEnumerable<PetInfoDto>?> SortByAgeAsync(int number = 0, int size = 15)
    {
        var userId = _identityService.GetUserIdentity();

        return await _innogotchiService.SortByAgeAsync(Guid.Parse(userId), number, size);
    }

    [Authorize]
    [HttpGet("sortByHunger")]
    public async Task<IEnumerable<PetInfoDto>?> SortByHungerLevelAsync(int number = 0, int size = 15)
    {
        var userId = _identityService.GetUserIdentity();

        return await _innogotchiService.SortByHungerLevelAsync(Guid.Parse(userId), number, size);
    }

    [Authorize]
    [HttpGet("sortByThirsty")]
    public async Task<IEnumerable<PetInfoDto>?> SortByWaterLevelAsync(int number = 0, int size = 15)
    {
        var userId = _identityService.GetUserIdentity();

        return await _innogotchiService.SortByWaterLevelAsync(Guid.Parse(userId), number, size);
    }

    [Authorize]
    [HttpGet]
    public async Task<InnogotchiDto?> GetByNameAsync(string name)
    {
        var userId = _identityService.GetUserIdentity();

        return await _innogotchiService.GetByNameAsync(Guid.Parse(userId), name);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm]InnogotchiDto innogotchiDto)
    {
        var userId = _identityService.GetUserIdentity();

        await _innogotchiService.CreateAsync(Guid.Parse(userId), innogotchiDto);

        return Ok(innogotchiDto);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(InnogotchiDto innogotchiDto)
    {
        await _innogotchiService.UpdateAsync(innogotchiDto);

        return Ok(innogotchiDto);
    }

    [Authorize]
    [HttpPut("feed")]
    public async Task FeedAsync(string name)
    {
        await _innogotchiStateService.FeedAsync(name);
    }

    [Authorize]
    [HttpPut("drink")]
    public async Task DrinkAsync(string name)
    {
        await _innogotchiStateService.DrinkAsync(name);
    }

    [Authorize]
    [HttpDelete]
    public async Task DeleteAsync(string name)
    {
        await _innogotchiService.DeleteAsync(name);
    }
}
