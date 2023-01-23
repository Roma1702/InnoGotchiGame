using Models.Core;

namespace DataAccessLayer.Abstraction.Interfaces;

public interface IUserRepository
{
    public Task<List<UserDto>> GetChunkAsync(int size, int number);
    public Task<UserDto?> GetByIdAsync(Guid id);
    public Task CreateAsync(UserDto userDto);
    public Task UpdateAsync(UserDto userDto);
    public Task UpdatePasswordAsync(Guid userId, string oldPassword, string newPassword);
    public Task DeleteAsync(Guid id);
}
