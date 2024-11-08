using JobseekBerca.Models;
using JobseekBerca.View;

namespace JobseekBerca.Repositories.Interfaces
{
    public interface IJobsRepository
    {
        IEnumerable<Applications> GetAllApplication();
        Applications GetApplicationById(string applicationId);
        int UpdateApplications(Applications applications);
    }
}
