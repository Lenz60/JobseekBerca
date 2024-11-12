using JobseekBerca.Models;
using JobseekBerca.ViewModels;

namespace JobseekBerca.Repositories.Interfaces
{
    public interface IApplicationsRepository
    {
        IEnumerable<Applications> GetAllApplications();
        Applications GetApplicationById(string applicationId);
        IEnumerable<ApplicationsVM.UserApplicationsVM> GetUserApplications(string userId);
        int AddApplications(Applications jobs);
        int UpdateApplications(Applications applications);
    }
}
