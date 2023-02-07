using Microsoft.AspNetCore.Mvc;
using Models.Core;

namespace Core.Abstraction.Interfaces;

public interface IUserService
{
    public Task<List<ShortUserDto>?> GetChunkAsync(Guid userId, int number, int size);
    public Task<ShortUserDto?> GetByIdAsync(Guid id);
    public Task<ShortUserDto?> GetByNameAsync(string name);
    public Task<IActionResult> SignUpAsync(UserDto userDto);
    public Task<IActionResult> UpdateAsync(Guid id, ShortUserDto userDto);
    public Task<IActionResult> UpdatePasswordAsync(Guid id, ChangePasswordDto changePasswordDto);
    public Task<IActionResult> DeleteAsync(Guid id);
    public Task<IActionResult> InviteAsync(Guid userId, string friendName);
    public Task<IActionResult> ConfirmAsync(Guid userId, string friendName);
}
