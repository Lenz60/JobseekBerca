using JobseekBerca.Helper;
using JobseekBerca.Models;
using JobseekBerca.Repositories;
using JobseekBerca.Repositories.Interfaces;
using JobseekBerca.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static JobseekBerca.ViewModels.DetailsVM;

namespace JobseekBerca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
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
            try
            {
                var data = _jobsRepository.GetAllJobs();
                return ResponseHTTP.CreateResponse(200, "Jobs fetched.", data);

            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(500, e.Message);
            }
        }

        [HttpGet("Detail")]
        public IActionResult GetJobs(string jobId)
        {
            try
            {
                var job = _jobsRepository.GetJobById(jobId);
                return ResponseHTTP.CreateResponse(200, "Jobs fetched.", job);
            }
            catch(HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);
            }
            catch(Exception e)
            {
                return ResponseHTTP.CreateResponse(500, e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddJobs(Jobs jobs)
        {
            var nullableFields = new HashSet<string> { "jobId" };
            if (Whitespace.HasNullOrEmptyStringProperties(jobs, out string propertyName, nullableFields))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                _jobsRepository.AddJobs(jobs);
                return ResponseHTTP.CreateResponse(200, "Success add new jobs", jobs);

            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(500, e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult DeleteJobs(JobVM.DeleteJobVM deleteVM)
        {
            try
            {
                _jobsRepository.DeleteJobs(deleteVM.userId, deleteVM.jobId);
                return ResponseHTTP.CreateResponse(200, "Success deleted job.");

            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(500, e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult UpdateJobs([FromBody] Jobs jobs)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(jobs, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                _jobsRepository.UpdateJobs(jobs);
                return ResponseHTTP.CreateResponse(200, "Job successfully updated.", jobs);

            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(500, e.Message);
            }
        }
    }
}
