namespace Entities.Entity;

public class Innogotchi
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public Guid FarmId { get; set; }
    public virtual Farm? Farm { get; set; }
    public virtual List<InnogotchiPart>? Parts { get; set; }
    public virtual InnogotchiState? InnogotchiState { get; set; }
}
