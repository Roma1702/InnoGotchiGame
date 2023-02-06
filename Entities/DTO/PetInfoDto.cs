using Models.Core;

namespace Contracts.DTO;

public class PetInfoDto
{
    public string? Name { get; set; }
    public InnogotchiStateDto? InnogotchiStateDto { get; set; }
}
