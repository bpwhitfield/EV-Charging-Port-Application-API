namespace EVApplicationAPI.Models;

public class UserApplicationDto
{
    public int ApplicationId { get; set; }

    public required string Name { get; set; } = string.Empty;

    public required string EmailAddress { get; set; } = string.Empty;

    public required string AddressLine1 { get; set; } = string.Empty;

    public string? AddressLine2 { get; set; }

    public required string City { get; set; } = string.Empty;

    public string? County { get; set; }

    public required string Postcode { get; set; } = string.Empty;

    public required string Vrn { get; set; } = string.Empty;
}