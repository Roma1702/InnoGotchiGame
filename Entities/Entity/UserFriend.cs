namespace Entities.Entity;

public class UserFriend
{
    public Guid UserId { get; set; }
    public Guid FriendId { get; set; }
    public bool IsVerified { get; set; }
}
