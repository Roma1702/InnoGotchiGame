using DataAccessLayer.Abstraction.Interfaces;
using DataAccessLayer.Data;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository;

public class UserFriendRepository : IUserFriendRepository
{
    private readonly DbSet<UserFriend> _dbSetUserFriend;
    private readonly ApplicationContext _context;

    public UserFriendRepository(ApplicationContext context)
    {
        _context = context;
        _dbSetUserFriend = context.Set<UserFriend>();
    }

    public async Task InviteAsync(Guid userId, Guid friendId)
    {
        if (!_dbSetUserFriend.Any(x => (x.UserId == userId || x.FriendId == friendId)
                        && (x.UserId == friendId || x.FriendId == friendId)))
        {
            await _dbSetUserFriend.AddAsync(new UserFriend
            {
                UserId = userId,
                FriendId = friendId,
                IsConfirmed = false
            });

            await _context.SaveChangesAsync();
        }
    }

    public async Task ConfirmAsync(Guid userId, Guid friendId)
    {
        var request = await _dbSetUserFriend.FirstOrDefaultAsync(x => x.UserId == friendId && x.FriendId == userId);

        request!.IsConfirmed = true;

        await _context.SaveChangesAsync();
    }

    public async Task RejectAsync(Guid userId, Guid friendId)
    {
        var request = await _dbSetUserFriend.FirstOrDefaultAsync(x => x.UserId == friendId && x.FriendId == userId);

        _dbSetUserFriend.Remove(request!);

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Guid>?> GetRequestsId(Guid userId)
    {
        var requests = await _dbSetUserFriend.Where(x => x.FriendId == userId && x.IsConfirmed == false)
            .Select(x => x.UserId).ToListAsync();

        return requests;
    }

    public async Task<IEnumerable<Guid>?> GetFriendsId(Guid userId)
    {
        var userFriends = await _dbSetUserFriend.Where(x => (x.UserId == userId
            || x.FriendId == userId) && x.IsConfirmed)
            .Select(x => new Guid(x.UserId == userId ? x.FriendId.ToByteArray() : x.UserId.ToByteArray()))
            .ToListAsync();

        return userFriends;
    }
}
