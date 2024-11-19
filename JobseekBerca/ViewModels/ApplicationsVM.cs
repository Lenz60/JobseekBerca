using JobseekBerca.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobseekBerca.ViewModels
{
    public class ApplicationsVM
    {
        public class UserApplicationsVM
        {
            public string jobId { get; set; }
            public string jobTitle { get; set; }
            public string jobType { get; set; }
            public string jobLocation { get; set; }
            public string jobRequirement { get; set; }
            public string jobSalary { get; set; }
            public string jobStatus { get; set; }
            public string applyDate { get; set; }
            public string dueDate { get; set; }

        }
        public class ApllicationsDetailVM
        {
            public string applicationId { get; set; }
            public string userId { get; set; }
            public string jobTitle { get; set; }
            public string fullName { get; set; }
            public string experience { get; set; }
            public string education { get; set; }
            public string skills { get; set; }
            public string? linkPersonalWebsite { get; set; }
            public string? linkGithub { get; set; }
            public string progress { get; set; }
        }
        public class ApplicationUpdateVM
        {
            public string applicationId { get; set; }
            public Status status { get; set; }

        }
    }
}
