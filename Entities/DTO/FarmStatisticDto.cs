namespace Contracts.DTO;

public class FarmStatisticDto
{
    public int CountOfAlivePets { get; set; }
    public int CountOfDeadPets { get; set; }
    public double AverageFeedPeriod { get; set; }
    public double AverageDrinking { get; set; }
    public double AverageHappinessDays { get; set; }
    public double AverageAge { get; set; }
}
