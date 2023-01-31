using Microsoft.AspNetCore.Http;

namespace Models.Core;

public class ShortUserDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public IFormFile? ProfilePhoto { get; set; }
    public FarmDto? FarmDto { get; set; }
}
