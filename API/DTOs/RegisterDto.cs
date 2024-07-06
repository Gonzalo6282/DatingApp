using System.ComponentModel.DataAnnotations;

namespace API.DTOs;//add folder path

public class RegisterDto
{
    [Required]//add data annotaion and use quick fix
    public string Username { get; set; } = string.Empty;

    [Required]//add data annotaion and use quick fix
    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; } = string.Empty;
}
