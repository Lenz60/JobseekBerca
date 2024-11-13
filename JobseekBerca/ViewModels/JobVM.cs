using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace JobseekBerca.ViewModels
{
    public class JobVM
    {
        public class DeleteJobVM
        {
            public string userId { get; set; }
            public string jobId { get; set; }
        }
    }
}
