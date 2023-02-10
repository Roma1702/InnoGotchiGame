using Entities.Identity;

namespace Entities.Entity;

public class Farm
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }
    public virtual ICollection<Innogotchi>? Pets { get; set; }
}
