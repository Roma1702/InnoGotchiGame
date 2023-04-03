using Models.Core;

namespace Core.Abstraction.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<ShortUserDto>?> GetRequestsAsync(Guid userId);
    public Task<ShortUserDto?> GetByIdAsync(Guid id);
    public Task<ShortUserDto?> GetByNameAsync(string name);
    public Task<bool> SignUpAsync(UserDto userDto);
    public Task<bool> UpdateAsync(ShortUserDto userDto);
    public Task<bool> UpdatePasswordAsync(Guid id, ChangePasswordDto changePasswordDto);
    public Task DeleteAsync(Guid id);
    public Task InviteAsync(Guid userId, string friendName);
    public Task ConfirmAsync(Guid userId, string friendName);
    public Task RejectAsync(Guid userId, string friendName);
}
