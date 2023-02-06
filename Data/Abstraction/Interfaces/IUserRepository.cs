using Entities.Identity;
using Models.Core;

namespace DataAccessLayer.Abstraction.Interfaces;

public interface IUserRepository
{
    public Task<List<ShortUserDto>?> GetChunkAsync(List<Guid> requestsId, int number, int size);
    public Task<ShortUserDto?> GetByIdAsync(Guid id);
    public Task<ShortUserDto?> GetByNameAsync(string name);
    public Task CreateAsync(UserDto userDto);
    public Task UpdateAsync(Guid id, ShortUserDto userDto);
    public Task UpdatePasswordAsync(Guid id, ChangePasswordDto changePasswordDto);
    public Task DeleteAsync(Guid id);
    public Task<User?> GetUserAsync(ShortUserDto userDto);
}
