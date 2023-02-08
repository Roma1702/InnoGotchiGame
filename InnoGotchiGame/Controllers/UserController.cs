using Core.Abstraction.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Core;

namespace InnoGotchiGame.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private readonly IUserService _userService;

    public UserController(IIdentityService identityService,
        IUserService userService)
	{
        _identityService = identityService;
        _userService = userService;
    }

    [Authorize]
    [HttpGet("id")]
    public async Task<ShortUserDto?> GetByNameAsync(Guid id)
    {
        return await _userService.GetByIdAsync(id);
    }

    [Authorize]
    [HttpGet]
    public async Task<ShortUserDto?> GetUserAsync()
    {
        var userId = _identityService.GetUserIdentity();

        return await _userService.GetByIdAsync(Guid.Parse(userId));
    }

    [Authorize]
    [HttpGet("name")]
    public async Task<ShortUserDto?> GetByNameAsync(string name)
    {
        return await _userService.GetByNameAsync(name);
    }

    [Authorize]
    [HttpGet("requests")]
    public async Task<List<ShortUserDto>?> GetChunkAsync(int number, int size)
    {
        var userId = _identityService.GetUserIdentity();

        return await _userService.GetChunkAsync(Guid.Parse(userId), number, size);
    }

    [HttpPost("register")]
    public async Task SignUpAsync([FromForm] UserDto userDto)
    {
        await _userService.SignUpAsync(userDto);
    }

    [Authorize]
    [HttpPost("invite")]
    public async Task InviteAsync(string friendName)
    {
        var userId = _identityService.GetUserIdentity();

        if (userId != string.Empty)
        {
            await _userService.InviteAsync(Guid.Parse(userId), friendName);
        }
    }

    [Authorize]
    [HttpPut("changePassword")]
    public async Task UpdatePasswordAsync(ChangePasswordDto changePasswordDto)
    {
        var userId = _identityService.GetUserIdentity();

        if (userId != string.Empty)
        {
            await _userService.UpdatePasswordAsync(Guid.Parse(userId), changePasswordDto);
        }
    }

    [Authorize]
    [HttpPut("edit")]
    public async Task UpdateAsync([FromForm] ShortUserDto userDto)
    {
        var userId = _identityService.GetUserIdentity();

        if (userId != string.Empty)
        {
            await _userService.UpdateAsync(Guid.Parse(userId), userDto);
        }
    }

    [Authorize]
    [HttpPut("confirm")]
    public async Task ConfirmAsync(string friendName)
    {
        var userId = _identityService.GetUserIdentity();

        if (userId != string.Empty)
        {
            await _userService.ConfirmAsync(Guid.Parse(userId), friendName);
        }
    }

    [Authorize]
    [HttpDelete("delete")]
    public async Task DeleteAsync()
    {
        var userId = _identityService.GetUserIdentity();

        if (userId != string.Empty)
        {
            await _userService.DeleteAsync(Guid.Parse(userId));
        }
    }
}
