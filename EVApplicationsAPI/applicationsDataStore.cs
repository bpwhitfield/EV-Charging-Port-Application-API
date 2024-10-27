using EVApplicationAPI.Models;
using Microsoft.Extensions.WebEncoders.Testing;

namespace EVApplicationAPI;

public class ApplicationsDataStore
{
    public List<UserApplicationDto> UserApplications { get; set; }
    public static ApplicationsDataStore Current { get; } = new ApplicationsDataStore();

    public ApplicationsDataStore()
    {
        UserApplications = new List<UserApplicationDto>()
        {
            new UserApplicationDto()
            {
                ApplicationId = 1,
                Name = "John Doe",
                EmailAddress = "johndoe@test.com",
                AddressLine1 = "address 1",
                City = "city",
                Postcode = "postcode",
                Vrn = "vrn"
            },
            new UserApplicationDto()
            {
                ApplicationId = 2,
                Name = "Ben Whitfield",
                EmailAddress = "bpwhitfield@test.com",
                AddressLine1 = "address 1",
                City = "city",
                Postcode = "postcode",
                Vrn = "vrn"
            }
        };
    }
}