using Entities.Identity;

namespace Entities.Entity;

public class Farm
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }
    public virtual ICollection<Innogotchi>? Pets { get; set; }
}
