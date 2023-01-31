using Entities.Identity;

namespace Entities.Entity;

public class UserFriend
{
    public Guid UserId { get; set; }
    public Guid FriendId { get; set; }
    public bool IsConfirmed { get; set; }
}