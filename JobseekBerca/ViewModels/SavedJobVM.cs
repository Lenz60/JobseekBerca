namespace JobseekBerca.ViewModels
{
    public class SavedJobVM
    {
        public class CreateVM
        {
            public string userId { get; set; }
            public string jobId { get; set; }
        }
        public class DeleteSavedJobVM
        {
            public string userId { get; set; }
            public string jobId { get; set; }
        }
        public class GetSaveJob
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
