using JobseekBerca.Helper;
using JobseekBerca.Models;
using JobseekBerca.Repositories;
using JobseekBerca.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JobseekBerca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class ApplicationsController : ControllerBase
    {
        private ApplicationsRepository _applicationsRepository;

        public ApplicationsController(ApplicationsRepository applicationsRepository)
        {
            _applicationsRepository = applicationsRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("All")]
        public IActionResult GetAllApplications()
        {
            try
            {
                var applications = _applicationsRepository.GetAllApplications();
                return ResponseHTTP.CreateResponse(200, "Applications retrieved successfully.", applications);

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

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult GetAllApplicationsById(string userId)
        {
            try
            {
                var applications = _applicationsRepository.GetUserApplications(userId);
                return ResponseHTTP.CreateResponse(200, "Applications retrieved successfully.", applications);

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

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult AddApplications(Applications applications)
        {
            var nullableFields = new HashSet<string> { "applicationId" };
            if (Whitespace.HasNullOrEmptyStringProperties(applications, out string propertyName,nullableFields))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                var addApplication = _applicationsRepository.AddApplications(applications);
                return ResponseHTTP.CreateResponse(200, "Success add new applications", applications);

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
        public IActionResult UpdateApplications(Applications applications)
        {
            //var nullableFields = new HashSet<string> { "credentialId", "credentialLink", "description" };
            if (Whitespace.HasNullOrEmptyStringProperties(applications, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                _applicationsRepository.UpdateApplications(applications);
                return ResponseHTTP.CreateResponse(200, "Application successfully updated.", applications);

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
