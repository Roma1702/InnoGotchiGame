namespace Models.Core;

public class InnogotchiDto
{
    public Guid Id { get; set; }
    public Guid FarmId { get; set; }
    public string? Name { get; set; }
    public MediaDto? Body { get; set; }
    public MediaDto? Eyes { get; set; }
    public MediaDto? Mouth { get; set; }
    public MediaDto? Nose { get; set; }
}
