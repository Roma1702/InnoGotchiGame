namespace Entities.Entity;

public class Innogotchi
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid FarmId { get; set; }
    public Farm? Farm { get; set; }
    public Guid BodyId { get; set; }
    public MediaInnogotchiPart? Body { get; set; }
    public Guid EyesId { get; set; }
    public MediaInnogotchiPart? Eyes { get; set; }
    public Guid MouthId { get; set; }
    public MediaInnogotchiPart? Mouth { get; set; }
    public Guid NoseId { get; set; }
    public MediaInnogotchiPart? Nose { get; set; }
    public InnogotchiState? InnogotchiState { get; set; }
}
