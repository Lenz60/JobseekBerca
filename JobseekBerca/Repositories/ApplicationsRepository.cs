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

        public const int INTERNAL_ERROR = -1;
        public const int SUCCESS = 1;
        public const int FAIL = 0;

        public ApplicationsRepository(MyContext myContext)
        {
            _myContext = myContext;
        }
        public int CheckUserId(string userId)
        {
            var check = _myContext.Users.Find(userId);
            if (check == null)
            {
                //return FAIL;
                throw new HttpResponseExceptionHelper(404, "User is not found");
            }
            return SUCCESS;
        }

        public IEnumerable<Applications> GetAllApplications()
        {
            try
            {
                return _myContext.Applications.ToList();
            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }

        public Applications GetApplicationById(string applicationId)
        {
            var selectedApplication = _myContext.Applications.Find(applicationId);
            if (selectedApplication != null)
            {
                return selectedApplication;
            }
            //return null;
            throw new HttpResponseExceptionHelper(404, "Application not found");
        }

        public int AddApplications(Applications applications)
        {
            try
            {
                var checkRole = _myContext.Users
                    .Where(x => x.userId == applications.userId)
                    .Select(u => u.roleId).FirstOrDefault();
                if (checkRole == "R03")
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
                throw new HttpResponseExceptionHelper(403, "Unauthorized access");
            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }

        public int UpdateApplications(Applications applications)
        {
            try
            {
                CheckUserId(applications.userId);
                var checkRole = _myContext.Users
                    .Where(x => x.userId == applications.userId)
                    .Select(u => u.roleId).FirstOrDefault();
                if (checkRole == "R02")
                {
                    var checkApplication = _myContext.Applications.Find(applications.applicationId);
                    if (checkApplication == null)
                    {
                        throw new HttpResponseExceptionHelper(404, "Invalid application id");
                    }
                    var newApplication = new Applications
                    {
                        applicationId = applications.applicationId,
                        status = applications.status,
                        jobId = applications.jobId,
                        userId = applications.userId,
                    };
                    _myContext.Entry(checkApplication).State = EntityState.Detached;
                    _myContext.Entry(newApplication).State = EntityState.Modified;
                    return _myContext.SaveChanges();
                }
                throw new HttpResponseExceptionHelper(403, "Unauthorized access");

            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }

        public IEnumerable<ApplicationsVM.UserApplicationsVM> GetUserApplications(string userId)
        {
            try
            {
                CheckUserId(userId);
                var applications = _myContext.Applications.Include(j => j.Jobs)
                    .Where(aj => aj.userId == userId)
                    .Select(aj => new ApplicationsVM.UserApplicationsVM
                    {
                        jobId = aj.jobId,
                        jobTitle = aj.Jobs.title,
                        jobType = aj.Jobs.type,
                        jobLocation = aj.Jobs.location,
                        jobRequirement = aj.Jobs.requirement,
                        jobSalary = aj.Jobs.salary,
                    }).ToList();
                if (applications == null)
                {
                    throw new HttpResponseExceptionHelper(404, "Application is not found");
                }
                return applications;
            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }
    }
}
