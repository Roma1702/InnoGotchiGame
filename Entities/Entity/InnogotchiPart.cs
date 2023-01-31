using static Contracts.Enum.Enums;

namespace Entities.Entity;

public class InnogotchiPart
{
    public Guid Id { get; set; }
    public virtual byte[]? Image { get; set; }
    public virtual ICollection<Innogotchi>? Bodies { get; set; }
    public virtual ICollection<Innogotchi>? Eyes { get; set; }
    public virtual ICollection<Innogotchi>? Mouths { get; set; }
    public virtual ICollection<Innogotchi>? Noses { get; set; }
    public PartType? PartType { get; set; }
}
