namespace JobseekBerca.ViewModels
{
    public class SavedJobVM
    {
        public class CreateVM
        {
            public string userId { get; set; }
            public string jobId { get; set; }
        }
        public class DeleteJobVM
        {
            public string userId { get; set; }
            public int savedJobId { get; set; }
        }
    }
}
