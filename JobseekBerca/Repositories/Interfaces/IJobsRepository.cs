using JobseekBerca.Models;
using JobseekBerca.View;

namespace JobseekBerca.Repositories.Interfaces
{
    public interface IJobsRepository
    {
        IEnumerable<Jobs> GetAllJobs();
        Jobs GetJobById(string jobId);
        int UpdateJobs(Jobs jobs);
        int DeleteJobs(string jobId);
        int AddJobs(Jobs jobs);
    }
}
