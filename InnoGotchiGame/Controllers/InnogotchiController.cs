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
    [HttpGet("{filter}/{number}/{size}")]
    public async Task<IEnumerable<PetInfoDto>?> GetFilterChunkAsync(int number, int size, string filter)
    {
        Dictionary<string, Func<Task<IEnumerable<PetInfoDto>?>>> dictFilters = new();
        var userId = _identityService.GetUserIdentity();

        dictFilters.Add("happyDays", new Func<Task<IEnumerable<PetInfoDto>?>>(async () => await _innogotchiService
            .SortByHappinessDays(Guid.Parse(userId), number, size)));

        dictFilters.Add("age", new Func<Task<IEnumerable<PetInfoDto>?>>(async () => await _innogotchiService
            .SortByAgeAsync(Guid.Parse(userId), number, size)));

        dictFilters.Add("hunger", new Func<Task<IEnumerable<PetInfoDto>?>>(async () => await _innogotchiService
            .SortByHungerLevelAsync(Guid.Parse(userId), number, size)));

        dictFilters.Add("thirsty", new Func<Task<IEnumerable<PetInfoDto>?>>(async () => await _innogotchiService
            .SortByWaterLevelAsync(Guid.Parse(userId), number, size)));

        return await dictFilters[filter]();
    }

    [Authorize]
    [HttpGet("{number}/{size}")]
    public async Task<IEnumerable<InnogotchiDto>?> GetChunkAsync(int number, int size)
    {
        var userId = _identityService.GetUserIdentity();

        return await _innogotchiService.GetChunkAsync(Guid.Parse(userId), number, size);
    }

    [Authorize]
    [HttpGet("count")]
    public async Task<int> GetCountAsync()
    {
        var userId = _identityService.GetUserIdentity();

        return await _innogotchiService.GetCountAsync(Guid.Parse(userId));
    }

    [Authorize]
    [HttpGet("{name}")]
    public async Task<InnogotchiDto?> GetByNameAsync(string name)
    {
        var userId = _identityService.GetUserIdentity();

        return await _innogotchiService.GetByNameAsync(Guid.Parse(userId), name);
    }

    [Authorize]
    [HttpGet("state/{name}")]
    public async Task<PetInfoDto?> GetStateByNameAsync(string name)
    {
        var userId = _identityService.GetUserIdentity();

        return await _innogotchiService.GetStateByNameAsync(Guid.Parse(userId), name);
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
    public async Task<IActionResult> UpdateAsync([FromForm] InnogotchiDto innogotchiDto)
    {
        await _innogotchiService.UpdateAsync(innogotchiDto);

        return Ok(innogotchiDto);
    }

    [Authorize]
    [HttpPut("feed/{name}")]
    public async Task<bool> FeedAsync(string name)
    {
        return await _innogotchiStateService.FeedAsync(name);
    }

    [Authorize]
    [HttpPut("drink/{name}")]
    public async Task<bool> DrinkAsync(string name)
    {
        return await _innogotchiStateService.DrinkAsync(name);
    }

    [Authorize]
    [HttpDelete("{name}")]
    public async Task DeleteAsync(string name)
    {
        await _innogotchiService.DeleteAsync(name);
    }
}
