using System.ComponentModel.DataAnnotations;

namespace EVApplicationAPI.Models;

public class UserApplicationUpdateDto
{
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    [EmailAddress]
    public required string EmailAddress { get; set; }

    public required string AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public required string City { get; set; }

    public string? County { get; set; }

    public required string Postcode { get; set; }

    public required string Vrn { get; set; }
}