using static Entities.Entity.InnogotchiState;

namespace Models.Core;


public class InnogotchiStateDto
{
    public Guid Id { get; set; }
    public Guid InnogotchiId { get; set; }
    public int Age { get; set; }
    public HungerLevel Hunger { get; set; }
    public ThirstyLevel Thirsty { get; set; }
    public DateTimeOffset Created { get; set; }
    public int HappyDays { get; set; }
}
