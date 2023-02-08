using Contracts.DTO;
using Core.Abstraction.Interfaces;
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

    [HttpGet("chunk")]
    public async Task<List<PetInfoDto>?> GetChunkAsync(int number = 0, int size = 15)
    {
        var userId = _identityService.GetUserIdentity();

        return await _innogotchiService.GetChunkAsync(Guid.Parse(userId), number, size);
    }

    [HttpGet]
    public async Task<InnogotchiDto?> GetByNameAsync(string name)
    {
        var userId = _identityService.GetUserIdentity();

        return await _innogotchiService.GetByNameAsync(Guid.Parse(userId), name);
    }

    [HttpPost("create")]
    public async Task CreateAsync([FromForm]InnogotchiDto innogotchiDto)
    {
        var userId = _identityService.GetUserIdentity();

        await _innogotchiService.CreateAsync(Guid.Parse(userId), innogotchiDto);
    }

    [HttpPut("update")]
    public async Task UpdateAsync(InnogotchiDto innogotchiDto)
    {
        await _innogotchiService.UpdateAsync(innogotchiDto);
    }

    [HttpPut("feed")]
    public async Task FeedAsync(string name)
    {
        await _innogotchiStateService.FeedAsync(name);
    }

    [HttpPut("drink")]
    public async Task DrinkAsync(string name)
    {
        await _innogotchiStateService.DrinkAsync(name);
    }

    [HttpDelete("delete")]
    public async Task DeleteAsync(string name)
    {
        await _innogotchiService.DeleteAsync(name);
    }
}
