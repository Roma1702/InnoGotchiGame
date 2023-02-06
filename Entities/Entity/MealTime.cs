using Entities.Entity;
using static Contracts.Enum.Enums;

namespace Contracts.Entity;

public class MealTime
{
    public Guid Id { get; set; }
    public Guid InnogotchiStateId { get; set; }
    public virtual InnogotchiState? InnogotchiState { get; set;}
    public MealType MealType { get; set; }
    public DateTimeOffset Time { get; set; }
}
