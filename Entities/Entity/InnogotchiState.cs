namespace Entities.Entity;

public class InnogotchiState
{
    public Guid Id { get; set; }
    public int Age { get; set; }
    public HungerLevel Hunger { get; set; }
    public ThirstyLevel Thirsty { get; set; }
    public Guid InnogotchiId { get; set; }
    public int HappyDays { get; set; }
    public Innogotchi? Innogotchi { get; set; }
    public enum HungerLevel
    {
        Full,
        Normal,
        Hunger,
        Dead
    }
    public enum ThirstyLevel
    {
        Full,
        Normal,
        Thirsty,
        Dead
    }
}
