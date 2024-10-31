using EVApplicationAPI.Entities;

namespace EVApplicationAPI.Services;

public interface IApplicationInfoRepository
{
    Task<IEnumerable<Application>> GetApplicationsAsync();

    Task<Application?> GetApplicationAsync(int applicationId);

    void AddApplication(Application application);

    void DeleteApplication(Application application);

    Task<bool> SaveChangesAsync();
}
