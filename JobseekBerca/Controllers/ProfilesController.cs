using JobseekBerca.Helper;
using JobseekBerca.Models;
using JobseekBerca.Repositories;
using JobseekBerca.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static JobseekBerca.ViewModels.ProfileVM;

namespace JobseekBerca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesRepository _profilesRepository;

        public ProfilesController(ProfilesRepository profilesRepository)
        {
            _profilesRepository = profilesRepository;
        }

        [HttpPost("GetById")]
        public IActionResult Get(string userId)
        {
            //return Ok();
            var check = _profilesRepository.GetProfile(userId);
            if (check != null)
            {
                return ResponseHTTP.CreateResponse(200, "User info is fetched!", check);
            }
            return ResponseHTTP.CreateResponse(400, "User info not found!");
        }

        [HttpPost("Update")]
        public IActionResult Update(ProfileVM.UpdateVM update)
        {
            if (string.IsNullOrWhiteSpace(update.userId))
            {
                return ResponseHTTP.CreateResponse(400, "User Id is required!");
            }
            if (string.IsNullOrWhiteSpace(update.fullName))
            {
                return ResponseHTTP.CreateResponse(400, "Full Name is required!");
            }
            //if (string.IsNullOrWhiteSpace(update.summary))
            //{
            //    return ResponseHTTP.CreateResponse(400, "Summary is required!");
            //}
            if (string.IsNullOrWhiteSpace(update.address))
            {
                return ResponseHTTP.CreateResponse(400, "Address is required!");
            }
            if (!Enum.IsDefined(typeof(Gender), update.gender) || string.IsNullOrWhiteSpace(update.gender.ToString()))
            {
                return ResponseHTTP.CreateResponse(400, "Gender is invalid!");
            }
            if (string.IsNullOrEmpty(update.birthDate.ToString()))
            {
                return ResponseHTTP.CreateResponse(400, "Birth Date is required!");
            }
            if (!DateTime.TryParse(update.birthDate.ToString(), out DateTime birthDate))
            {
                return ResponseHTTP.CreateResponse(400, "Birth Date is invalid!");
            }


            var check = _profilesRepository.GetProfile(update.userId);
            if (check != null)
            {
                try
                {
                    var updateProfile = _profilesRepository.UpdateProfile(update);
                    if (updateProfile > 0)
                    {
                        return ResponseHTTP.CreateResponse(200, "User info is updated!", update);
                    }

                }
                catch (Exception e)
                {
                    return ResponseHTTP.CreateResponse(400, e.Message);
                }
                return ResponseHTTP.CreateResponse(400, "Fail to update user info!");
            }
            return ResponseHTTP.CreateResponse(404, "User info is not found");

        }
    }
}
