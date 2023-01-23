using Entities.Entity.Abstraction;
using Entities.Identity;

namespace Entities.Entity;

public class MediaUser : Media
{
    public virtual ICollection<User>? Users { get; set; }
}
