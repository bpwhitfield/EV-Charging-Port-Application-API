using EVApplicationAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EVApplicationAPI.DbContexts;

public class ApplicationInfoContext(DbContextOptions<ApplicationInfoContext> options)
    : DbContext(options)
{
    public DbSet<Application> Applications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>()
            .HasData(
            new Application("Ben Whitfield", "test@tester.com", "4 Porden rd", "London", "SW2 5RT", "BS20 BLS")
            {
                ApplicationId = 1,
                Name = "Ben",
                EmailAddress = "test@tester.com",
                AddressLine1 = "",
                AddressLine2 = "Brixton",
                City = "",
                Postcode = "",
                County = "Greater London",
                Vrn = ""
            });
        base.OnModelCreating(modelBuilder);
    }
}