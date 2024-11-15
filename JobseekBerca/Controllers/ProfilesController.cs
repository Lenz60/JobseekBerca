using JobseekBerca.Helper;
using JobseekBerca.Models;
using JobseekBerca.Repositories;
using JobseekBerca.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static JobseekBerca.ViewModels.ProfileVM;

namespace JobseekBerca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    [Authorize(Roles = "User")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesRepository _profilesRepository;

        public ProfilesController(ProfilesRepository profilesRepository)
        {
            _profilesRepository = profilesRepository;
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult Get(string userId)
        {
            //var nullableFields = new HashSet<string> { "credentialId", "credentialLink", "description" };
            if (Whitespace.HasNullOrEmptyStringProperties(userId, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                var profile = _profilesRepository.GetProfile(userId);
                return ResponseHTTP.CreateResponse(200, "User info is fetched!", profile);

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

        [HttpPut("Update")]
        public IActionResult Update(ProfileVM.UpdateVM update)
        {

            //if (!Enum.IsDefined(typeof(Gender), update.gender) || string.IsNullOrWhiteSpace(update.gender.ToString()))
            //{
            //    return ResponseHTTP.CreateResponse(400, "Gender is invalid!");
            //}
            //if (string.IsNullOrEmpty(update.birthDate.ToString()))
            //{
            //    return ResponseHTTP.CreateResponse(400, "Birth Date is required!");
            //}
            //if (!DateTime.TryParse(update.birthDate.ToString(), out DateTime birthDate))
            //{
            //    return ResponseHTTP.CreateResponse(400, "Birth Date is invalid!");
            //}
            var nullableFields = new HashSet<string> { "summary", "address", "linkPersonalWebsite", "profileImage", "linkGithub" };
            if (Whitespace.HasNullOrEmptyStringProperties(update, out string propertyName, nullableFields))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                _profilesRepository.UpdateProfile(update);
                return ResponseHTTP.CreateResponse(200, "User info is updated!", update);

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
