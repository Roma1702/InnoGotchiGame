using Models.Core;

namespace DataAccessLayer.Abstraction.Interfaces;

public interface IUserFriendRepository
{
    public Task InviteAsync(Guid userId, Guid friendId);
    public Task ConfirmAsync(Guid userId, Guid friendId);
    public Task<List<Guid>?> GetRequests(Guid userId);
}
