namespace EVApplicationAPI.Models;

public class UserApplicationDto
{
    public int ApplicationId { get; set; }

    public required string Name { get; set; }

    public required string EmailAddress { get; set; }
}