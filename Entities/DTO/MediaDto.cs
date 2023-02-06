using Microsoft.AspNetCore.Http;

namespace Models.Core;

public class MediaDto
{
    public IFormFile? Image { get; set; }
}
