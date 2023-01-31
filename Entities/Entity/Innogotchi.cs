namespace Entities.Entity;

public class Innogotchi
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid FarmId { get; set; }
    public virtual Farm? Farm { get; set; }
    public Guid BodyId { get; set; }
    public virtual InnogotchiPart? Body { get; set; }
    public Guid EyesId { get; set; }
    public virtual InnogotchiPart? Eyes { get; set; }
    public Guid MouthId { get; set; }
    public virtual InnogotchiPart? Mouth { get; set; }
    public Guid NoseId { get; set; }
    public virtual InnogotchiPart? Nose { get; set; }
    public virtual InnogotchiState? InnogotchiState { get; set; }
}
