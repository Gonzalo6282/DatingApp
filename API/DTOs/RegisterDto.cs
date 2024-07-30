﻿using System.ComponentModel.DataAnnotations;

namespace API.DTOs;//add folder path

public class RegisterDto
{
    [Required]//add data annotaion and use quick fix
    public string Username { get; set; } = string.Empty;
    [Required] public string? KnownAs { get; set; }
    [Required] public string? Gender { get; set; }
    [Required] public string? DateOfBirth { get; set; }
    [Required] public string? City { get; set; }
    [Required] public string? Country { get; set; }
    
    [Required]//add data annotaion and use quick fix
    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; } = string.Empty;
}
