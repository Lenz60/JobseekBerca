using JobseekBerca.Context;
using JobseekBerca.Models;
using JobseekBerca.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using JobseekBerca.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.IIS.Core;
using JobseekBerca.Helper;

namespace JobseekBerca.Repositories
{
    public class SavedJobsRepository : ISavedJobsRepository
    {
        private readonly MyContext _myContext;

        public SavedJobsRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public const int FAIL = 0;
        public const int SUCCESS = 1;
        public const int INTERNAL_ERROR = -1;

        public int CheckUserId(string userId)
        {
            var check = _myContext.Users.Find(userId);
            if (check == null)
            {
                throw new HttpResponseExceptionHelper(404, "User not found");
            }
            return SUCCESS;
        }
        public int CreateSavedJob(string userId, string jobId)
        {
            try
            {
                CheckUserId(userId);
                var checkRole = _myContext.Users
                    .Where(x => x.userId == userId)
                    .Select(u => u.roleId).FirstOrDefault();
                if (checkRole == "R03")
                {
                    var checkJob = _myContext.Jobs.Find(jobId);
                    if (checkJob == null)
                    {
                        throw new HttpResponseExceptionHelper(404, "Job not found");
                    }
                    var savedJob = new SavedJobs
                    {
                        userId = userId,
                        jobId = jobId
                    };
                    _myContext.SavedJobs.Add(savedJob);
                    _myContext.SaveChanges();
                    return SUCCESS;
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

        public int DeleteSavedJob(SavedJobVM.DeleteSavedJobVM deleteVM)
        {
            var check = CheckUserId(deleteVM.userId);
            if (check == FAIL)
            {
                //throw new Exception("Invalid user");
                //return FAIL;
                throw new HttpResponseExceptionHelper(403, "Invalid User");
            }
            // TODO , Fix the find 
            else
            {
                var checkRole = _myContext.Users
                    .Where(x => x.userId == deleteVM.userId)
                    .Select(u => u.roleId).FirstOrDefault();
                if (checkRole == "R03")
                {
                    var savedJob = _myContext.SavedJobs
                        .Where(sj => sj.userId == deleteVM.userId && sj.jobId == deleteVM.jobId)
                        .FirstOrDefault();

                    if (savedJob == null)
                    {
                        //return FAIL;
                        //throw new Exception("Job not found");
                        throw new HttpResponseExceptionHelper(404, "Job not found");
                    }
                    _myContext.SavedJobs.Remove(savedJob);
                    _myContext.SaveChanges();
                    return SUCCESS;
                }
                //throw new Exception("Unauthorized");
                throw new HttpResponseExceptionHelper(401, "Unauthorized");
            }
        }

        public IEnumerable<SavedJobVM.GetSaveJob> GetSavedJobs(string userId)
        {
            var check = CheckUserId(userId);
            if (check == FAIL)
            {
                //return null;
                //throw new Exception("Invalid user");
                throw new HttpResponseExceptionHelper(403, "Invalid User");
            }
            else
            {
                var savedJobs = _myContext.SavedJobs
                .Where(x => x.userId == userId)
                .Select(savedJob => new SavedJobVM.GetSaveJob
                {
                    jobId = savedJob.Job.jobId,
                    jobTitle = savedJob.Job.title,
                    jobType = savedJob.Job.type,
                    jobLocation = savedJob.Job.location,
                    jobRequirement = savedJob.Job.requirement,
                    jobSalary = savedJob.Job.salary,
                    postDate = savedJob.Job.postDate.HasValue ? savedJob.Job.postDate.Value.ToString("yyyy-MM-dd") : "N/A",
                    dueDate = savedJob.Job.dueDate.HasValue ? savedJob.Job.dueDate.Value.ToString("yyyy-MM-dd") : "N/A"
                }).ToList();

                //if (savedJobs == null || !savedJobs.Any())
                //{
                //    //return null;
                //    //throw new Exception("No saved jobs found");
                //    throw new HttpResponseExceptionHelper(401, "No saved jobs found");
                //}
                return savedJobs;
            }

        }
    }
}
