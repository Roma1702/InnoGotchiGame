using Entities.Entity;
using Microsoft.AspNetCore.Identity;

namespace Entities.Identity;

public class User : IdentityUser<Guid>
{
    public override Guid Id { get; set; } = Guid.NewGuid();
    public virtual Farm? Farm { get; set; }
    public byte[]? ProfilePhoto { get; set; }
    public string? FileExtension { get; set; }
}