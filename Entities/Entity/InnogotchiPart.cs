using static Contracts.Enum.Enums;

namespace Entities.Entity;

public class InnogotchiPart
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public virtual byte[]? Image { get; set; }
    public string? FileExtension { get; set; }
    public Guid InnogotchiId { get; set; }
    public virtual Innogotchi? Innogotchi { get; set; }
    public PartType? PartType { get; set; }
}
