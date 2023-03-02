namespace DataAccessLayer.Abstraction.Interfaces;

public interface IUserFriendRepository
{
    public Task InviteAsync(Guid userId, Guid friendId);
    public Task ConfirmAsync(Guid userId, Guid friendId);
    public Task RejectAsync(Guid userId, Guid friendId);
    public Task<IEnumerable<Guid>?> GetRequestsId(Guid userId);
    public Task<IEnumerable<Guid>?> GetFriendsId(Guid userId);
}
