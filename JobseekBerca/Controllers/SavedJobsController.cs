using JobseekBerca.Helper;
using JobseekBerca.Models;
using JobseekBerca.Repositories;
using JobseekBerca.Repositories.Interfaces;
using JobseekBerca.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobseekBerca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedJobsController : ControllerBase
    {
        private readonly SavedJobsRepository _savedJobsRepository;

        public SavedJobsController(SavedJobsRepository savedJobsRepository)
        {
            _savedJobsRepository = savedJobsRepository;
        }


        [HttpGet]
        public IActionResult GetSavedJobs(string userId)
        {
            try
            {
                var result = _savedJobsRepository.GetSavedJobs(userId);
                return ResponseHTTP.CreateResponse(200, "Saved jobs fetched.", result);

            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);
            }
            //if (result == null)
            //{
            //}
        }

        [HttpPost]
        public IActionResult CreateSavedJob(SavedJobVM.CreateVM savedJobVM)
        {
            try
            {
                var result = _savedJobsRepository.CreateSavedJob(savedJobVM.userId, savedJobVM.jobId);
                return ResponseHTTP.CreateResponse(200, "Job saved.");

            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);

            }
        }

        [HttpDelete]
        public IActionResult DeleteSavedJob([FromBody] SavedJobVM.DeleteJobVM savedJob)
        {
            try
            {
                var result = _savedJobsRepository.DeleteSavedJob(savedJob);
                return ResponseHTTP.CreateResponse(200, "Job deleted.");

            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);

            }
            //if (result > 0)
            //{
            //}
        }
    }
}
