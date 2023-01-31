using Microsoft.AspNetCore.Http;

namespace Models.Core;

public class UserDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public IFormFile? ProfilePhoto { get; set; }
}
