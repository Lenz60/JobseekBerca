using JobseekBerca.Context;
using Microsoft.Identity.Client;
using JobseekBerca.Repositories.Interfaces;
using JobseekBerca.ViewModels;
using JobseekBerca.Models;
using JobseekBerca.Helper;
using Microsoft.EntityFrameworkCore;

namespace JobseekBerca.Repositories
{
    public class ApplicationsRepository : IApplicationsRepository
    {
        private readonly MyContext _myContext;

        public int FAIL { get; private set; }

        public ApplicationsRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public IEnumerable<Applications> GetAllApplications()
        {
            return _myContext.Applications.ToList();
        }

        public Applications GetApplicationById(string applicationId)
        {
            var selectedApplication = _myContext.Applications.Find(applicationId);
            if (selectedApplication != null)
            {
            return selectedApplication;
            }
            return null;
        }

        public int AddApplications(Applications applications)
        {
            var checkId = _myContext.Applications.OrderByDescending(a => a.applicationId).FirstOrDefault();

            if (checkId != null)
            {
                int lastId = int.Parse(checkId.applicationId.Substring(1));
                applications.applicationId = "A" + (lastId + 1).ToString("D2");
            }
            else
            {
                applications.applicationId = "A01";
            }
            _myContext.Applications.Add(applications);
            return _myContext.SaveChanges();
        }

        public int UpdateApplications(Applications applications)
        {
            var exists = _myContext.Applications.Any(a => a.applicationId == applications.applicationId);

            if (exists)
            {
                _myContext.Entry(applications).State = EntityState.Modified;
                return _myContext.SaveChanges();
            }

            return FAIL;
        }
    }
}
