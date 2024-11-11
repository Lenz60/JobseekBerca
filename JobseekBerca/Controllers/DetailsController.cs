using JobseekBerca.Helper;
using JobseekBerca.Models;
using JobseekBerca.Repositories;
using JobseekBerca.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobseekBerca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        public readonly CertificatesRepository _certificateRepository;
        public readonly EducationsRepository _educationsRepository;
        public readonly ExperiencesRepository _experiencesRepository;
        public readonly SkillsRepository _skillsRepository;

        public DetailsController(CertificatesRepository certificateRepository, 
            EducationsRepository educationsRepository, 
            ExperiencesRepository experiencesRepository, 
            SkillsRepository skillsRepository)
        {
            _certificateRepository = certificateRepository;
            _educationsRepository = educationsRepository;
            _experiencesRepository = experiencesRepository;
            _skillsRepository = skillsRepository;
        }

        [HttpGet("Experiences")]
        public IActionResult GetExperiences(string userId)
        {
            var check = _experiencesRepository.GetExperienceById(userId);
            if (check != null)
            {
                return ResponseHTTP.CreateResponse(200, "Experiences info fetched!", check);
            }
            return ResponseHTTP.CreateResponse(404, "Experiences info is not found!");
        }

        [HttpPost("Experiences")]
        public IActionResult CreateExperiences(Experiences experiences)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(experiences, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            var check = _experiencesRepository.CheckUserId(experiences.userId);
            if (check > 0 )
            {
                var updateExperiences = _experiencesRepository.CreateExperience(experiences);
                if (updateExperiences > 0)
                {
                    return ResponseHTTP.CreateResponse(200, "Experiences info is updated!", experiences);
                }
                return ResponseHTTP.CreateResponse(400, "Fail to update experiences info!");
            }
            return ResponseHTTP.CreateResponse(404, "User is not found");
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
            var educations = _educationsRepository.GetEducationById(userId);
            if (educations != null)
            {
                return ResponseHTTP.CreateResponse(200, "Educations info fetched!", educations);
            }
            return ResponseHTTP.CreateResponse(404, "Educations info is not found!");
        }

        [HttpPost("Educations")]
        public IActionResult CreateEducations(Educations educations)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(educations, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            var check = _educationsRepository.CheckUserId(educations.userId);
            if (check > 0)
            {
                var updateEducations = _educationsRepository.CreateEducation(educations);
                if (updateEducations > 0)
                {
                    return ResponseHTTP.CreateResponse(200, "Educations info is updated!", educations);
                }
                return ResponseHTTP.CreateResponse(400, "Fail to update educations info!");
            }
            return ResponseHTTP.CreateResponse(404, "User info is not found");
        }

        [HttpDelete("Educations")]
        public IActionResult DeleteEducations(DetailsVM.DeleteVM deleteVM)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(deleteVM, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            var check = _educationsRepository.GetEducationById(deleteVM.userId);
            if (check != null)
            {
                var deleteEducations = _educationsRepository.DeleteEducation(deleteVM);
                if (deleteEducations > 0)
                {
                    return ResponseHTTP.CreateResponse(200, "Educations info is deleted!");
                }
                return ResponseHTTP.CreateResponse(400, "Fail to delete educations info!");
            }
            return ResponseHTTP.CreateResponse(404, "Educations info is not found!");
        }

        [HttpGet("Skills")]
        public IActionResult GetSkills(string userId)
        {
            var skills = _skillsRepository.GetSkillById(userId);
            if (skills != null)
            {
                return ResponseHTTP.CreateResponse(200, "Skills info fetched!", skills);
            }
            return ResponseHTTP.CreateResponse(404, "Skills info is not found!");
        }

        [HttpPost("Skills")]
        public IActionResult CreateSkills(Skills skills)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(skills, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            var check = _skillsRepository.CheckUserId(skills.userId);
            if (check > 0)
            {
                var updateSkills = _skillsRepository.CreateSkill(skills);
                if (updateSkills > 0)
                {
                    return ResponseHTTP.CreateResponse(200, "Skills info is updated!", skills);
                }
                return ResponseHTTP.CreateResponse(400, "Fail to update skills info!");
            }
            return ResponseHTTP.CreateResponse(404, "User info is not found");
        }

        [HttpDelete("Skills")]
        public IActionResult DeleteSkills(DetailsVM.DeleteVM deleteVM)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(deleteVM, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            var check = _skillsRepository.GetSkillById(deleteVM.userId);
            if (check != null)
            {
                var deleteSkills = _skillsRepository.DeleteSkill(deleteVM);
                if (deleteSkills > 0)
                {
                    return ResponseHTTP.CreateResponse(200, "Skills info is deleted!");
                }
                return ResponseHTTP.CreateResponse(400, "Fail to delete skills info!");
            }
            return ResponseHTTP.CreateResponse(404, "Skills info is not found!");
        }

        [HttpGet("Certificates")]
        public IActionResult GetCertificates(string userId)
        {
            var certificates = _certificateRepository.GetCertificateById(userId);
            if (certificates != null)
            {
                return ResponseHTTP.CreateResponse(200, "Certificates info fetched!", certificates);
            }
            return ResponseHTTP.CreateResponse(404, "Certificates info is not found!");
        }


        [HttpPost("Certificates")]
        public IActionResult CreateCertificates(Certificates certificate)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(certificate, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            var check = _certificateRepository.CheckUserId(certificate.userId);
            if (check > 0)
            {
                var updateCertificate = _certificateRepository.CreateCertificate(certificate);
                if (updateCertificate > 0)
                {
                    return ResponseHTTP.CreateResponse(200, "Certificate info is updated!", certificate);
                }
                return ResponseHTTP.CreateResponse(400, "Fail to update certificate info!");
            }
            return ResponseHTTP.CreateResponse(404, "User info is not found");
        }

        [HttpDelete("Certificates")]
        public IActionResult DeleteCertificates(DetailsVM.DeleteVM deleteVM)
        {
            if (Whitespace.HasNullOrEmptyStringProperties(deleteVM, out string propertyName))
            {
                return ResponseHTTP.CreateResponse(400, $"{propertyName} is required!");
            }
            var check = _certificateRepository.GetCertificateById(deleteVM.userId);
            if (check != null)
            {
                var deleteCertificates = _certificateRepository.DeleteCertificate(deleteVM);
                if (deleteCertificates > 0)
                {
                    return ResponseHTTP.CreateResponse(200, "Certificates info is deleted!");
                }
                return ResponseHTTP.CreateResponse(400, "Fail to delete certificates info!");
            }
            return ResponseHTTP.CreateResponse(404, "Certificates info is not found!");
        }
    }
}
