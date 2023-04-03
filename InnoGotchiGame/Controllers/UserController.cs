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

    //[Authorize]
    //[HttpGet("{id}")]
    //public async Task<ShortUserDto?> GetByIdAsync(Guid id)
    //{
    //    return await _userService.GetByIdAsync(id);
    //}

    [Authorize]
    [HttpGet]
    public async Task<ShortUserDto?> GetUserAsync()
    {
        var userId = _identityService.GetUserIdentity();

        return await _userService.GetByIdAsync(Guid.Parse(userId));
    }

    [Authorize]
    [HttpGet("{name}")]
    public async Task<ShortUserDto?> GetByNameAsync(string name)
    {
        return await _userService.GetByNameAsync(name);
    }

    [Authorize]
    [HttpGet("requests")]
    public async Task<IEnumerable<ShortUserDto>?> GetRequestsAsync()
    {
        var userId = _identityService.GetUserIdentity();

        return await _userService.GetRequestsAsync(Guid.Parse(userId));
    }

    [HttpPost("registration")]
    public async Task<IActionResult> SignUpAsync([FromForm] UserDto userDto)
    {
       var result =  await _userService.SignUpAsync(userDto);

        if (result) return Ok(result);

        return BadRequest();
    }

    [Authorize]
    [HttpPost("invite/{friendName}")]
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
    public async Task<IActionResult> UpdatePasswordAsync([FromForm] ChangePasswordDto changePasswordDto)
    {
        var userId = _identityService.GetUserIdentity();

        var result = await _userService.UpdatePasswordAsync(Guid.Parse(userId), changePasswordDto);

        if (result) return Ok(result);

        return BadRequest();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromForm] ShortUserDto userDto)
    {
        var result = await _userService.UpdateAsync(userDto);

        if (result) return Ok(result);

        return Ok(userDto);
    }

    [Authorize]
    [HttpPut("confirm/{friendName}")]
    public async Task ConfirmAsync(string friendName)
    {
        var userId = _identityService.GetUserIdentity();

        if (userId != string.Empty)
        {
            await _userService.ConfirmAsync(Guid.Parse(userId), friendName);
        }
    }

    [Authorize]
    [HttpDelete("reject/{friendName}")]
    public async Task RejectAsync(string friendName)
    {
        var userId = _identityService.GetUserIdentity();

        if (userId != string.Empty)
        {
            await _userService.RejectAsync(Guid.Parse(userId), friendName);
        }
    }

    [Authorize]
    [HttpDelete]
    public async Task DeleteAsync()
    {
        var userId = _identityService.GetUserIdentity();

        if (userId != string.Empty)
        {
            await _userService.DeleteAsync(Guid.Parse(userId));
        }
    }
}
