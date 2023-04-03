using Entities.Identity;
using Models.Core;

namespace DataAccessLayer.Abstraction.Interfaces;

public interface IUserRepository
{
    public Task<IEnumerable<ShortUserDto>?> GetRequestsAsync(IEnumerable<Guid> requestsId);
    public Task<ShortUserDto?> GetByIdAsync(Guid id);
    public Task<ShortUserDto?> GetByNameAsync(string name);
    public Task CreateAsync(UserDto userDto);
    public Task UpdateAsync(ShortUserDto userDto);
    public Task UpdatePasswordAsync(Guid id, ChangePasswordDto changePasswordDto);
    public Task DeleteAsync(Guid id);
    public Task<User?> GetUserAsync(ShortUserDto userDto);
}
