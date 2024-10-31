using EVApplicationAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EVApplicationAPI.DbContexts;

public class ApplicationInfoContext(DbContextOptions<ApplicationInfoContext> options)
    : DbContext(options)
{
    public DbSet<Application> Applications { get; set; }
}