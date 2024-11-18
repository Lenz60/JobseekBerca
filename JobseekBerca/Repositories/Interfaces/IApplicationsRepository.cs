using JobseekBerca.Models;
using JobseekBerca.ViewModels;
using static JobseekBerca.ViewModels.ApplicationsVM;

namespace JobseekBerca.Repositories.Interfaces
{
    public interface IApplicationsRepository
    {
        IEnumerable<Applications> GetAllApplications();
        IEnumerable<ApllicationsDetailVM> GetAllApplicationsDetail();
        Applications GetApplicationById(string applicationId);
        IEnumerable<ApplicationsVM.UserApplicationsVM> GetUserApplications(string userId);
        int AddApplications(Applications jobs);
        int UpdateApplications(ApplicationUpdateVM applications);
    }
}
