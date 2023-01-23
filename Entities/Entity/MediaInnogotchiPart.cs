using Entities.Entity.Abstraction;

namespace Entities.Entity;

public class MediaInnogotchiPart : Media
{
    public virtual ICollection<Innogotchi>? Pets { get; set; }
    public InnogotchiPartType PartType { get; set; }
}
public enum InnogotchiPartType
{
    Body,
    Nose,
    Eyes,
    Mouth
}
