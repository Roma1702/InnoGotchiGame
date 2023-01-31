using Microsoft.AspNetCore.Http;

namespace Models.Core;

public class MediaDto
{
    public Guid Id { get; set; }
    public IFormFile? Image { get; set; }
}
