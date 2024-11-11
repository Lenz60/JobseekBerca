using JobseekBerca.Helper;
using JobseekBerca.Models;
using JobseekBerca.Repositories;
using JobseekBerca.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static JobseekBerca.ViewModels.DetailsVM;

namespace JobseekBerca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private JobsRepository _jobsRepository;

        public JobsController(JobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        [HttpGet]
        public IActionResult GetAllJobs()
        {
            var data = _jobsRepository.GetAllJobs();

            if (data == null)
            {
                return ResponseHTTP.CreateResponse(200, "No job found.");
            }
            else
            {
                return ResponseHTTP.CreateResponse(200, "Jobs fetched.", data);
            }
        }

        [HttpPost]
        public IActionResult AddJobs(Jobs jobs)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(jobs, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            var addJob = _jobsRepository.AddJobs(jobs);

            if (addJob > 0)
            {
                return ResponseHTTP.CreateResponse(200, "Success add new jobs", jobs);
            }
            else
            {
                return ResponseHTTP.CreateResponse(404, "Job Failed added");
            }
        }

        [HttpDelete("{jobId}")]
        public IActionResult DeleteJobs(string jobId)
        {
            int result = _jobsRepository.DeleteJobs(jobId);
            if (result > 0)
            {
                return ResponseHTTP.CreateResponse(200, "Success deleted job.");
            }
            return ResponseHTTP.CreateResponse(404, "No job found with the sepecific id.");
        }

        [HttpPut]
        public IActionResult UpdateJobs([FromBody] Jobs jobs)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(jobs, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            int data = _jobsRepository.UpdateJobs(jobs);

            if (data > 0)
            {
                return ResponseHTTP.CreateResponse(200, "Job successfully updated.", jobs);
            }
            return ResponseHTTP.CreateResponse(404, "No jobs found with the sepecific id.");
        }
    }
}
