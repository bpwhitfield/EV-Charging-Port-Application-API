using System.ComponentModel.DataAnnotations;

namespace EVApplicationAPI.Models;

public class UserApplicationUpdateDto
{
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    [EmailAddress]
    public required string EmailAddress { get; set; }
}