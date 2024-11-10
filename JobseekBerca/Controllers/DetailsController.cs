using JobseekBerca.Helper;
using JobseekBerca.Models;
using JobseekBerca.Repositories;
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
        [HttpPost("Experiences")]
        public IActionResult Create(Experiences experiences)
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

        [HttpPost("Educations")]
        public IActionResult Create(Educations educations)
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

        [HttpPost("Skills")]
        public IActionResult Create(Skills skills)
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

        [HttpPost("Certificates")]
        public IActionResult Create(Certificates certificate)
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
    }
}
