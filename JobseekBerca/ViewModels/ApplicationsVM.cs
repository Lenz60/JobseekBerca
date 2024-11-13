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
        }
    }
}
