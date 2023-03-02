namespace Contracts.Enum;

public class Enums
{
    public enum HungerLevel
    {
        Full = 1,
        Normal = 2,
        Hunger = 3,
        Dead = 4
    }
    public enum ThirstyLevel
    {
        Full = 1,
        Normal = 2,
        Thirsty = 3,
        Dead = 4
    }
    public enum PartType
    {
        Body = 1,
        Eyes = 2,
        Mouth = 3,
        Nose = 4
    }
    public enum MealType
    {
        Feeding,
        Drinking
    }
}