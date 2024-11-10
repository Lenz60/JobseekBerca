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
    public class ApplicationsController : ControllerBase
    {
        private ApplicationsRepository _applicationsRepository;

        public ApplicationsController(ApplicationsRepository applicationsRepository)
        {
            _applicationsRepository = applicationsRepository;
        }

        [HttpGet]
        public IActionResult GetAllApplications()
        {
            var data = _applicationsRepository.GetAllApplications();

            if (data == null)
            {
                return ResponseHTTP.CreateResponse(200, "No data Applications.");
            }
            else
            {
                return ResponseHTTP.CreateResponse(200, "Applications retrieved successfully.", data);
            }
        }

        [HttpPost]
        public IActionResult AddApplications(Applications applications)
        {
            if (string.IsNullOrEmpty(applications.status))
            {
                return ResponseHTTP.CreateResponse(400, "Status is required!");
            }
            if (string.IsNullOrWhiteSpace(applications.jobId))
            {
                return StatusCode(200, new
                {
                    StatusCode = 200,
                    Message = $"Job Id is Required"
                });
            }
            if (string.IsNullOrWhiteSpace(applications.userId))
            {
                return StatusCode(200, new
                {
                    StatusCode = 200,
                    Message = $"User Id is Required"
                });
            }
            var addApplication = _applicationsRepository.AddApplications(applications);

            if (addApplication > 0)
            {
                return ResponseHTTP.CreateResponse(200, "Success add new applications", applications);
            }
            else
            {
                return ResponseHTTP.CreateResponse(404, "Application Failed added");
            }
        }

        [HttpPut("{applicationId}")]
        public IActionResult UpdateApplications(string applicationId, [FromBody] Applications applications)
        {
            if (string.IsNullOrEmpty(applications.applicationId))
            {
                return ResponseHTTP.CreateResponse(400, "ApplicationID is required!");
            }else if (string.IsNullOrEmpty(applications.status))
            {
                return ResponseHTTP.CreateResponse(400, "Status is required!");
            }
            int data = _applicationsRepository.UpdateApplications(applications);

            if (data > 0)
            {
                return ResponseHTTP.CreateResponse(200, "Application successfully updated.", applications);
            }
            return ResponseHTTP.CreateResponse(404, "No applications found with the sepecific id.");
        }
    }
}
