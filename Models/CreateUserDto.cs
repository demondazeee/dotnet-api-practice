using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models;

public class CreateUserDto
{   
    [Required]
    [MaxLength(12)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 100)]
    public int Age { get; set; }
}