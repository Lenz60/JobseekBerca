using JobseekBerca.Helper;
using JobseekBerca.Models;
using JobseekBerca.Repositories;
using JobseekBerca.Repositories.Details_Repository;
using JobseekBerca.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static JobseekBerca.ViewModels.DetailsVM;

namespace JobseekBerca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    [Authorize(Roles = "User,Admin")]
    public class DetailsController : ControllerBase
    {
        public readonly CertificatesRepository _certificateRepository;
        public readonly EducationsRepository _educationsRepository;
        public readonly ExperiencesRepository _experiencesRepository;
        public readonly SkillsRepository _skillsRepository;
        public readonly SocialMediaRepository _socialMediaRepository;

        public DetailsController(CertificatesRepository certificateRepository,
            EducationsRepository educationsRepository,
            ExperiencesRepository experiencesRepository,
            SkillsRepository skillsRepository,
            SocialMediaRepository socialMediaRepository)
        {
            _certificateRepository = certificateRepository;
            _educationsRepository = educationsRepository;
            _experiencesRepository = experiencesRepository;
            _skillsRepository = skillsRepository;
            _socialMediaRepository = socialMediaRepository;
        }

        [HttpGet("Experiences")]
        public IActionResult GetExperiences(string userId)
        {
            try
            {
                var experience = _experiencesRepository.GetExperienceById(userId);
                return ResponseHTTP.CreateResponse(200, "Experiences info fetched!", experience);
            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(500, e.Message);
            }
            //if (check != null)
            //{
            //}
            //return ResponseHTTP.CreateResponse(404, "Experiences info is not found!");
        }

        [HttpPost("Experiences")]
        public IActionResult CreateExperiences(Experiences experiences)
        {
            var nullableFields = new HashSet<string> { "endDate", "description" };
            if (Whitespace.HasNullOrEmptyStringProperties(experiences, out string propertyName, nullableFields))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                var updateExperiences = _experiencesRepository.CreateExperience(experiences);
                return ResponseHTTP.CreateResponse(200, "Experiences info is updated!", experiences);

            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(500, e.Message);
            }
            //var check = _experiencesRepository.CheckUserId(experiences.userId);
            //if (check > 0)
            //{
            //}
            //return ResponseHTTP.CreateResponse(404, "User is not found");
        }

        [HttpPut("Experiences")]
        public IActionResult UpdateExperiences(Experiences experiences)
        {
            var nullableFields = new HashSet<string> { "endDate", "description" };
            if (Whitespace.HasNullOrEmptyStringProperties(experiences, out string propertyName, nullableFields))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                var updateExperiences = _experiencesRepository.UpdateExperience(experiences);
                return ResponseHTTP.CreateResponse(200, "Experiences info is updated!", experiences);
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

        [HttpDelete("Experiences")]
        public IActionResult DeleteExperiences(DetailsVM.DeleteVM deleteVM)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(deleteVM, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            var check = _experiencesRepository.GetExperienceById(deleteVM.userId);
            if (check != null)
            {
                var deleteExperiences = _experiencesRepository.DeleteExperience(deleteVM);
                if (deleteExperiences > 0)
                {
                    return ResponseHTTP.CreateResponse(200, "Experiences info is deleted!");
                }
                return ResponseHTTP.CreateResponse(400, "Fail to delete experiences info!");
            }
            return ResponseHTTP.CreateResponse(404, "Experiences info is not found!");
        }

        [HttpGet("Educations")]
        public IActionResult GetEducations(string userId)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(userId, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                var educations = _educationsRepository.GetEducationById(userId);
                return ResponseHTTP.CreateResponse(200, "Educations info fetched!", educations);

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

        [HttpPost("Educations")]
        public IActionResult CreateEducations(Educations educations)
        {
            var nullableFields = new HashSet<string> { "description", "endDate" };
            if (Whitespace.HasNullOrEmptyStringProperties(educations, out string propertyName, nullableFields))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                var updateEducations = _educationsRepository.CreateEducation(educations);
                return ResponseHTTP.CreateResponse(200, "Educations info is updated!", educations);

            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);

            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(500, e.Message);

            }
            //if (check > 0)s
        }

        [HttpPut("Educations")]
        public IActionResult UpdateEducations(Educations educations)
        {

            var nullableFields = new HashSet<string> { "description", "endDate" };
            if (Whitespace.HasNullOrEmptyStringProperties(educations, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                //var check = _educationsRepository.GetEducationById(educations.userId);
                _educationsRepository.UpdateEducation(educations);
                return ResponseHTTP.CreateResponse(200, "Educations info is updated!", educations);

            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);

            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(500, e.Message);

            }
            //if (check != null)
            //{
            //    if (updateEducations > 0)
            //    {
            //    }
            //}
            //return ResponseHTTP.CreateResponse(404, "Educations info is not found!");
        }

        [HttpDelete("Educations")]
        public IActionResult DeleteEducations(DetailsVM.DeleteVM deleteVM)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(deleteVM, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                //var check = _educationsRepository.GetEducationById(deleteVM.userId);
                _educationsRepository.DeleteEducation(deleteVM);
                return ResponseHTTP.CreateResponse(200, "Educations info is deleted!");

            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);

            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(500, e.Message);

            }
            //if (check != null)
            //{
            //    if (deleteEducations > 0)
            //    {
            //    }
            //}
            //return ResponseHTTP.CreateResponse(404, "Educations info is not found!");
        }

        [HttpGet("Skills")]
        public IActionResult GetSkills(string userId)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(userId, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                var skills = _skillsRepository.GetSkillById(userId);
                return ResponseHTTP.CreateResponse(200, "Skills info fetched!", skills);
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

        [HttpPost("Skills")]
        public IActionResult CreateSkills(Skills skills)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(skills, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                var updateSkills = _skillsRepository.CreateSkill(skills);
                return ResponseHTTP.CreateResponse(200, "Skills info is updated!", skills);
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

        [HttpPut("Skills")]
        public IActionResult UpdateSkills(Skills skills)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(skills, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                //var check = _skillsRepository.GetSkillById(skills.userId);
                var updateSkills = _skillsRepository.UpdateSkill(skills);
                return ResponseHTTP.CreateResponse(200, "Skills info is updated!", skills);
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

        [HttpDelete("Skills")]
        public IActionResult DeleteSkills(DetailsVM.DeleteVM deleteVM)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(deleteVM, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                _skillsRepository.DeleteSkill(deleteVM);
                return ResponseHTTP.CreateResponse(200, "Skills info is deleted!");
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

        [HttpGet("Certificates")]
        public IActionResult GetCertificates(string userId)
        {
            try
            {
                var certificates = _certificateRepository.GetCertificateById(userId);
                return ResponseHTTP.CreateResponse(200, "Certificates info fetched!", certificates);

            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(404, e.Message);

            }
            //if (certificates != null)
            //{
            //}
        }


        [HttpPost("Certificates")]
        public IActionResult CreateCertificates(Certificates certificate)
        {
            var nullableFields = new HashSet<string> { "credentialId", "credentialLink", "description" };
            if (Whitespace.HasNullOrEmptyStringProperties(certificate, out string propertyName, nullableFields))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                var updateCertificate = _certificateRepository.CreateCertificate(certificate);
                return ResponseHTTP.CreateResponse(200, "Certificate info is updated!", certificate);
            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message, certificate);
            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(400, e.Message);
            }
        }

        [HttpPut("Certificates")]
        public IActionResult UpdateCertificates(Certificates certificates)
        {
            var nullableFields = new HashSet<string> { "credentialId", "credentialLink", "description" };
            if (Whitespace.HasNullOrEmptyStringProperties(certificates, out string propertyName, nullableFields))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                var updateCertificates = _certificateRepository.UpdateCertificate(certificates);
                return ResponseHTTP.CreateResponse(200, "Certificates info is updated!", certificates);
            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(400, e.Message);
            }
        }

        [HttpDelete("Certificates")]
        public IActionResult DeleteCertificates(DetailsVM.DeleteVM deleteVM)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(deleteVM, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                var check = _certificateRepository.GetCertificateById(deleteVM.userId);
                var deleteCertificates = _certificateRepository.DeleteCertificate(deleteVM);
                return ResponseHTTP.CreateResponse(200, "Certificates info is deleted!");
            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);

            }
        }

        [HttpGet("Social")]
        public IActionResult GetSosmed(string userId)
        {
            try
            {
                var socialMedias = _socialMediaRepository.GetSocialMediasById(userId);
                return ResponseHTTP.CreateResponse(200, "Social media info fetched!", socialMedias);
            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(400, e.Message);
            }
        }

        [HttpPost("Social")]
        public IActionResult CreateSosmed(SocialMedias socialMedia)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(socialMedia, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                var createSocialMedia = _socialMediaRepository.CreateSocialMedia(socialMedia);
                return ResponseHTTP.CreateResponse(200, "Social media info is created!", socialMedia);
            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(400, e.Message);
            }
        }

        [HttpPut("Social")]
        public IActionResult UpdateSosmed(SocialMedias socialMedia)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(socialMedia, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                var updateSocialMedia = _socialMediaRepository.UpdateSocialMedia(socialMedia);
                return ResponseHTTP.CreateResponse(200, "Social media info is updated!", socialMedia);
            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(400, e.Message);
            }
        }

        [HttpDelete("Social")]
        public IActionResult DeleteSosmed(DeleteVM deleteVM)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(deleteVM, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            try
            {
                var deleteSocialMedia = _socialMediaRepository.DeleteSocialMedia(deleteVM);
                return ResponseHTTP.CreateResponse(200, "Social media info is deleted!");
            }
            catch (HttpResponseExceptionHelper e)
            {
                return ResponseHTTP.CreateResponse(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return ResponseHTTP.CreateResponse(400, e.Message);
            }
        }
    }
}
