using Entities.Entity;
using Microsoft.AspNetCore.Identity;

namespace Entities.Identity;

public class User : IdentityUser<Guid>
{
    public Guid PhotoId { get; set; }
    public virtual MediaUser? Photo { get; set; }
    public Farm? Farm { get; set; }
}
