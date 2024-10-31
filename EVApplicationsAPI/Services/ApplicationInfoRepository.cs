using EVApplicationAPI.DbContexts;
using EVApplicationAPI.Entities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace EVApplicationAPI.Services;

public class ApplicationInfoRepository: IApplicationInfoRepository
{
    private readonly ApplicationInfoContext _context;

    public ApplicationInfoRepository(ApplicationInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Application>> GetApplicationsAsync()
    {
        return await _context.Applications.OrderBy(c => c.ApplicationId).ToListAsync();
    }

    public async Task<Application?> GetApplicationAsync(int applicationId)
    {
        return await _context.Applications.Where(c => c.ApplicationId == applicationId).FirstOrDefaultAsync();
    }

    public void AddApplication(Application application)
    {
        _context.Applications.Add(application);
    }

    public void DeleteApplication(Application application)
    {
        _context.Applications.Remove(application);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() >= 0;
    }
}