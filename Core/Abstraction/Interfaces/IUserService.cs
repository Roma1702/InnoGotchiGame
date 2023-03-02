using Models.Core;

namespace Core.Abstraction.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<ShortUserDto>?> GetChunkAsync(Guid userId, int number, int size);
    public Task<ShortUserDto?> GetByIdAsync(Guid id);
    public Task<ShortUserDto?> GetByNameAsync(string name);
    public Task SignUpAsync(UserDto userDto);
    public Task UpdateAsync(Guid id, ShortUserDto userDto);
    public Task UpdatePasswordAsync(Guid id, ChangePasswordDto changePasswordDto);
    public Task DeleteAsync(Guid id);
    public Task InviteAsync(Guid userId, string friendName);
    public Task ConfirmAsync(Guid userId, string friendName);
    public Task RejectAsync(Guid userId, string friendName);
}
