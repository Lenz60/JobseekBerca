using JobseekBerca.Context;
using Microsoft.Identity.Client;
using JobseekBerca.Repositories.Interfaces;
using JobseekBerca.ViewModels;
using JobseekBerca.Models;
using JobseekBerca.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace JobseekBerca.Repositories
{
    public class JobsRepository : IJobsRepository
    {
        private readonly MyContext _myContext;

        public const int INTERNAL_ERROR = -1;
        public const int SUCCESS = 1;
        public const int FAIL = 0;

        public JobsRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public IEnumerable<Jobs> GetAllJobs()
        {
            return _myContext.Jobs.ToList();
        }

        public Jobs GetJobById(string jobId)
        {
            try
            {
                var selectedJob = _myContext.Jobs.Find(jobId);
                return selectedJob == null ? throw new HttpResponseExceptionHelper(404, "Job not found") : selectedJob;
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

        public int AddJobs(Jobs jobs)
        {
            try
            {
                var checkRole = _myContext.Users
                    .Where(x => x.userId == jobs.userId)
                    .Select(u => u.roleId).FirstOrDefault();
                if (checkRole == "R02")
                {
                    //var checkId = _myContext.Jobs.OrderByDescending(j => j.jobId).FirstOrDefault();

                    //if (checkId != null)
                    //{
                    //    int lastId = int.Parse(checkId.jobId.Substring(1));
                    //    jobs.jobId = "J" + (lastId + 1).ToString("D2");
                    //}
                    //else
                    //{
                    //    jobs.jobId = "J01";
                    //}
                    jobs.jobId = ULIDHelper.GenerateULID();
                    _myContext.Jobs.Add(jobs);
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

        public int UpdateJobs(Jobs jobs)
        {
            try
            {
                var checkRole = _myContext.Users
                    .Where(x => x.userId == jobs.userId)
                    .Select(u => u.roleId).FirstOrDefault();
                if (checkRole == "R02")
                {
                    var checkJob = _myContext.Jobs.Find(jobs.jobId);
                    if (checkJob == null)
                    {
                        throw new HttpResponseExceptionHelper(404, "Invalid job id");
                    }
                    var newJob = new Jobs
                    {
                        jobId = jobs.jobId,
                        title = jobs.title,
                        description = jobs.description,
                        type = jobs.type,
                        salary = jobs.salary,
                        requirement = jobs.requirement,
                        location = jobs.location,
                        userId = jobs.userId
                    };
                    _myContext.Entry(checkJob).State = EntityState.Detached;
                    _myContext.Entry(newJob).State = EntityState.Modified;
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

        public int DeleteJobs(string userId, string jobId)
        {
            try
            {
                var checkRole = _myContext.Users
                        .Where(x => x.userId == userId)
                        .Select(u => u.roleId).FirstOrDefault();
                if (checkRole == "R02")
                {
                    var data = _myContext.Jobs.Find(jobId);
                    if (data != null)
                    {
                        _myContext.Jobs.Remove(data);
                        return _myContext.SaveChanges();
                    }
                    //return FAIL;
                    throw new HttpResponseExceptionHelper(404, "Invalid job id");
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
    }
}
