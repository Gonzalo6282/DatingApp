using System.ComponentModel.DataAnnotations;

namespace API.DTOs;//add folder path

public class LoginDto
{
    [Required]//add data annotaion and use quick fix
    public required string Username { get; set; }

    [Required]//add data annotaion and use quick fix
    public required string Password { get; set; }

}
