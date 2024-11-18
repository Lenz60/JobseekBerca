using JobseekBerca.Context;
using Microsoft.Identity.Client;
using JobseekBerca.Repositories.Interfaces;
using JobseekBerca.ViewModels;
using JobseekBerca.Models;
using JobseekBerca.Helper;
using Microsoft.EntityFrameworkCore;
using static JobseekBerca.ViewModels.ApplicationsVM;
using static JobseekBerca.ViewModels.UserVM;

namespace JobseekBerca.Repositories
{
    public class ApplicationsRepository : IApplicationsRepository
    {
        private readonly MyContext _myContext;
        private readonly SMTPHelper _smtpHelper;

        public const int INTERNAL_ERROR = -1;
        public const int SUCCESS = 1;
        public const int FAIL = 0;

        public ApplicationsRepository(MyContext myContext, SMTPHelper smtphelper)
        {
            _myContext = myContext;
            _smtpHelper = smtphelper;
        }
        public int CheckUserId(string userId)
        {
            var check = _myContext.Users.Find(userId);
            if (check == null)
            {
                //return FAIL;
                throw new HttpResponseExceptionHelper(404, "User is not found");
            }
            return SUCCESS;
        }

        public IEnumerable<Applications> GetAllApplications()
        {
            try
            {
                return _myContext.Applications.ToList();
            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }

        public IEnumerable<ApllicationsDetailVM> GetAllApplicationsDetail()
        {
            return _myContext.Applications
                .Include(app => app.Jobs)
                .ThenInclude(job => job.Users.Profiles)
                .ThenInclude(profile => profile.Experiences)
                .Include(app => app.Users.Profiles.Educations)
                .Include(app => app.Users.Profiles.Skills)
                .Select(app => new
                {
                    UserId = app.Users.userId,
                    JobId = app.Jobs.jobId,
                    JobTitle = app.Jobs.title,
                    FullName = app.Users.Profiles.fullName,
                    Experiences = app.Users.Profiles.Experiences.Select(e => e.position),
                    Educations = app.Users.Profiles.Educations.Select(e => e.universityName),
                    Skills = app.Users.Profiles.Skills.Select(s => s.skillName),
                    PersonalWebsite = app.Users.Profiles.linkPersonalWebsite,
                    Github = app.Users.Profiles.linkGithub,
                    Status = app.status
                })
                .GroupBy(app => new { app.JobId, app.JobTitle, app.FullName, app.UserId })
                .Select(group => new ApllicationsDetailVM
                {
                    userId = group.Key.UserId,
                    jobTitle = group.Key.JobTitle,
                    fullName = group.Key.FullName,
                    experience = string.Join(", ", group.SelectMany(g => g.Experiences).Distinct()),
                    education = string.Join(", ", group.SelectMany(g => g.Educations).Distinct()),
                    skills = string.Join(", ", group.SelectMany(g => g.Skills).Distinct()),
                    linkPersonalWebsite = group.FirstOrDefault().PersonalWebsite,
                    linkGithub = group.FirstOrDefault().Github,
                    progress = string.Join(", ", group.Select(g => g.Status).Distinct())
                });
        }


        public Applications GetApplicationById(string applicationId)
        {
            var selectedApplication = _myContext.Applications.Find(applicationId);
            if (selectedApplication != null)
            {
                return selectedApplication;
            }
            //return null;
            throw new HttpResponseExceptionHelper(404, "Application not found");
        }

        public int AddApplications(Applications applications)
        {
            try
            {
                var checkRole = _myContext.Users
                    .Where(x => x.userId == applications.userId)
                    .Select(u => u.roleId).FirstOrDefault();
                if (checkRole == "R03")
                {
                    var checkId = _myContext.Applications.OrderByDescending(a => a.applicationId).FirstOrDefault();
                    if (checkId != null)
                    {
                        int lastId = int.Parse(checkId.applicationId.Substring(1));
                        applications.applicationId = "A" + (lastId + 1).ToString("D2");
                    }
                    else
                    {
                        applications.applicationId = "A01";
                    }
                    _myContext.Applications.Add(applications);
                    return _myContext.SaveChanges();
                }
                throw new HttpResponseExceptionHelper(403, "Unauthorized access");
            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }

        private void SendApplicationStatusEmail(Users user, Jobs job, Profiles userDetail, Status status)
        {
            var toEmail = user.email ?? throw new ArgumentNullException(nameof(user.email));
            var subject = $"BerCareer Application: {job.title}";
            string body = "";

            if (status == Status.Approved)
            {
                body = $@"
                <html>
                    <head>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f9;
                                margin: 0;
                                padding: 0;
                            }}
                            .container {{
                                width: 100%;
                                padding: 20px;
                                background-color: #ffffff;
                                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                margin: 20px auto;
                                max-width: 600px;
                            }}
                            .header {{
                                background-color: #ffffff;
                                color: white;
                                padding: 10px 0;
                                text-align: center;
                            }}
                            .content {{
                                padding: 20px;
                            }}
                            .footer {{
                                text-align: center;
                                padding: 10px;
                                font-size: 12px;
                                color: #888;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <div class='header'>
                                <h1><img src='https://i.imgur.com/lzIYAOJ.png' alt='Company Logo'></h1>
                            </div>
                            <div class='content'>
                                <h2>{job.title}</h2>
                                <h4>Berca Hardayaperkasa - {job.location}</h4>   
                                <p>Dear {userDetail.fullName}</p>
                                <p>We are pleased to inform you that after reviewing your application and interview, we have selected you for the {job.title} position at Berca Hardayaperkasa. 
                                    Congratulations!</p>
                                <p>Your skills and experience impressed us, and we are excited to welcome you to our team. 
                                    We believe you will make a valuable contribution to our projects and collaborate well with our development team.</p>
                                <p>We will be reaching out shortly to discuss the next steps, including your start date, onboarding process, and other details related to your role. 
                                    In the meantime, if you have any questions or need further information, feel free to reach out to us..</p>
                                <p>Once again, congratulations, and we look forward to working with you soon!</p>
                            </div>
                            <div class='footer'>
                                <p>&copy; 2024 BerCareer. All rights reserved.</p>
                            </div>
                        </div>
                    </body>
                </html>";
            }
            else if (status == Status.Rejected)
            {
                body = $@"
                <html>
                    <head>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f9;
                                margin: 0;
                                padding: 0;
                            }}
                            .container {{
                                width: 100%;
                                padding: 20px;
                                background-color: #ffffff;
                                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                margin: 20px auto;
                                max-width: 600px;
                            }}
                            .header {{
                                background-color: #ffffff;
                                color: white;
                                padding: 10px 0;
                                text-align: center;
                            }}
                            .content {{
                                padding: 20px;
                            }}
                            .footer {{
                                text-align: center;
                                padding: 10px;
                                font-size: 12px;
                                color: #888;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <div class='header'>
                                <h1><img src='https://i.imgur.com/lzIYAOJ.png' alt='Company Logo' ></h1>
                            </div>
                            <div class='content'>
                                <h2>{job.title}</h2>
                                <h4>Berca Hardayaperkasa - {job.location}</h4>   
                                <p>Dear {userDetail.fullName}</p> 
                                <p>Thank you for your interest in joining Berca Hardayaperkasa as <strong>{job.title}</strong>.
                                   Unfortunately, we have decided <strong>not to proceed with your application</strong> at this time.
                                    </p>
                                <p>We see that you have a strong potential to grow and we believe you will flourish more in your next career. 
                                    We wish you all the best in your future endeavors.
                                    </p>
                            </div>
                            <div class='footer'>
                                <p>&copy; 2024 BerCareer. All rights reserved.</p>
                            </div>
                        </div>
                    </body>
                </html>";
            }

            try
            {
                _smtpHelper.SendEmail(toEmail, subject, body);

            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
        }


        public int UpdateApplications(ApplicationsVM.ApplicationUpdateVM applicationVM)
        {
            try
            {
                var userCheck = _myContext.Applications.Find(applicationVM.aplicationId);
                CheckUserId(userCheck.userId);
                //var checkRole = _myContext.Users
                //    .Where(x => x.userId == applications.userId)
                //    .Select(u => u.roleId).FirstOrDefault();
                var applicationUpdate = _myContext.Applications.Find(applicationVM.aplicationId);
                if (applicationUpdate == null)
                {
                    throw new HttpResponseExceptionHelper(404, "Invalid application id");
                }
                //var newApplication = new Applications
                //{
                //    applicationId = applications.aplicationId,
                //    status = applications.status,
                //};
                applicationUpdate.status = applicationVM.status;
                try
                {
                    var application = _myContext.Applications.Find(applicationVM.aplicationId);
                    var user = _myContext.Users.Find(application.userId);
                    var job = _myContext.Jobs.Find(application.jobId);
                    var userDetail = _myContext.Profiles.Include(u => u.Users).Where(u => u.Users.userId == user.userId).FirstOrDefault();

                    // Send email
                    SendApplicationStatusEmail(user, job, userDetail, applicationVM.status);
                }
                catch (HttpResponseExceptionHelper e)
                {
                    throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
                }

                //_myContext.Entry(checkApplication).State = EntityState.Detached;
                _myContext.Entry(applicationUpdate).State = EntityState.Modified;
                return _myContext.SaveChanges();

                throw new HttpResponseExceptionHelper(403, "Unauthorized access");

            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }


        public IEnumerable<ApplicationsVM.UserApplicationsVM> GetUserApplications(string userId)
        {
            try
            {
                CheckUserId(userId);
                var applications = _myContext.Applications.Include(j => j.Jobs)
                    .Where(aj => aj.userId == userId)
                    .Select(aj => new ApplicationsVM.UserApplicationsVM
                    {
                        jobId = aj.jobId,
                        jobTitle = aj.Jobs.title,
                        jobType = aj.Jobs.type,
                        jobLocation = aj.Jobs.location,
                        jobRequirement = aj.Jobs.requirement,
                        jobSalary = aj.Jobs.salary,
                        jobStatus = aj.status.ToString(),
                        applyDate = aj.applyDate.HasValue ? aj.applyDate.Value.ToString("yyyy-MM-dd") : null,
                        dueDate = aj.Jobs.dueDate.HasValue ? aj.Jobs.dueDate.Value.ToString("yyyy-MM-dd") : null

                    }).ToList();
                if (applications == null)
                {
                    throw new HttpResponseExceptionHelper(404, "Application is not found");
                }
                return applications;
            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }
    }
}
