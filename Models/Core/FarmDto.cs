namespace Models.Core;

public class FarmDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid UserId { get; set; }
    public virtual List<InnogotchiDto>? PetsDto { get; set; }
}
