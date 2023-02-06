using Contracts.Entity;
using static Contracts.Enum.Enums;

namespace Entities.Entity;

public class InnogotchiState
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Age { get; set; }
    public HungerLevel Hunger { get; set; }
    public ThirstyLevel Thirsty { get; set; }
    public virtual ICollection<MealTime>? MealTimes { get; set; }
    public DateTimeOffset StartOfHappinessDays { get; set; }
    public int HappinessDays { get; set; }
    public DateTimeOffset Created { get; set; }
    public Guid InnogotchiId { get; set; }
    public virtual Innogotchi? Innogotchi { get; set; }
}
