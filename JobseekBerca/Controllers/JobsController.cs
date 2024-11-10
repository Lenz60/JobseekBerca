using JobseekBerca.Helper;
using JobseekBerca.Models;
using JobseekBerca.Repositories;
using JobseekBerca.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
                return ResponseHTTP.CreateResponse(200, "No data Roles.");
            }
            else
            {
                return ResponseHTTP.CreateResponse(200, "Roles retrieved successfully.", data);
            }
        }

        [HttpPost]
        public IActionResult AddJobs(Jobs jobs)
        {
            if (string.IsNullOrEmpty(jobs.title))
            {
                return ResponseHTTP.CreateResponse(400, "Title is required!");
            }
            if (string.IsNullOrEmpty(jobs.description))
            {
                return ResponseHTTP.CreateResponse(400, "Description is required!");
            }
            if (string.IsNullOrEmpty(jobs.salary))
            {
                return ResponseHTTP.CreateResponse(400, "Salary is required!");
            }
            if (string.IsNullOrEmpty(jobs.location))
            {
                return ResponseHTTP.CreateResponse(400, "Location is required!");
            }
            if (string.IsNullOrWhiteSpace(jobs.userId))
            {
                return StatusCode(200, new
                {
                    StatusCode = 200,
                    Message = $"User Id is Required"
                });
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

        [HttpPut("{jobId}")]
        public IActionResult UpdateJobs(string jobId, [FromBody] Jobs jobs)
        {
            if (string.IsNullOrEmpty(jobs.jobId))
            {
                return ResponseHTTP.CreateResponse(400, "JobID is required!");
            }else if (string.IsNullOrEmpty(jobs.title))
            {
                return ResponseHTTP.CreateResponse(400, "Title is required!");
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
