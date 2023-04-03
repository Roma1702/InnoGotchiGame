using static Contracts.Enum.Enums;

namespace Models.Core;

public class MediaDto
{
    public string? Image { get; set; }
    public string? FileExtension { get; set; }
    public PartType? PartType { get; set; }
}
