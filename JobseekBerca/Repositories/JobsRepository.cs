using JobseekBerca.Context;
using Microsoft.Identity.Client;
using JobseekBerca.Repositories.Interfaces;
using JobseekBerca.ViewModels;
using JobseekBerca.Models;
using JobseekBerca.Helper;
using Microsoft.EntityFrameworkCore;

namespace JobseekBerca.Repositories
{
    public class JobsRepository : IJobsRepository
    {
        private readonly MyContext _myContext;

        public int FAIL { get; private set; }

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
            var selectedJob = _myContext.Jobs.Find(jobId);
            if (selectedJob != null)
            {
            return selectedJob;
            }
            return null;
        }

        public int AddJobs(Jobs jobs)
        {
            var checkId = _myContext.Jobs.OrderByDescending(j => j.jobId).FirstOrDefault();

            if (checkId != null)
            {
                int lastId = int.Parse(checkId.jobId.Substring(1));
                jobs.jobId = "J" + (lastId + 1).ToString("D2");
            }
            else
            {
                jobs.jobId = "J01";
            }
            _myContext.Jobs.Add(jobs);
            return _myContext.SaveChanges();
        }

        public int UpdateJobs(Jobs jobs)
        {
            var exists = _myContext.Jobs.Any(j => j.jobId == jobs.jobId);

            if (exists)
            {
                _myContext.Entry(jobs).State = EntityState.Modified;
                return _myContext.SaveChanges();
            }

            return FAIL;
        }

        public int DeleteJobs(string jobId)
        {
            var data = _myContext.Jobs.Find(jobId);
            if (data != null)
            {
                _myContext.Jobs.Remove(data);
                return _myContext.SaveChanges();
            }
            return FAIL;
        }
    }
}
