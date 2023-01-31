using static Contracts.Enum.Enums;

namespace Entities.Entity;

public class InnogotchiState
{
    public Guid Id { get; set; }
    public int Age { get; set; }
    public HungerLevel Hunger { get; set; }
    public ThirstyLevel Thirsty { get; set; }
    public Guid InnogotchiId { get; set; }
    public int HappyDays { get; set; }
    public DateTimeOffset Created { get; set; }
    public virtual Innogotchi? Innogotchi { get; set; }
}
