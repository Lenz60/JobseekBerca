using JobseekBerca.Models;

namespace JobseekBerca.Repositories.Interfaces
{
    public interface IApplicationsRepository
    {
        IEnumerable<Applications> GetAllApplications();
        Applications GetApplicationById(string applicationId);
        int AddApplications(Applications jobs);
        int UpdateApplications(Applications applications);
    }
}
