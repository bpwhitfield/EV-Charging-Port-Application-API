using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVApplicationAPI.Entities;

public class Application(string name, string emailAddress, string addressLine1,
    string city, string postcode, string vrn)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ApplicationId { get; set; }

    public required string Name { get; set; } = name;

    public required string EmailAddress { get; set; } = emailAddress;

    public required string AddressLine1 { get; set; } = addressLine1;

    public string? AddressLine2 { get; set; }

    public required string City { get; set; } = city;

    public string? County { get; set; }

    public required string Postcode { get; set; } = postcode;

    public required string Vrn { get; set; } = vrn;
}