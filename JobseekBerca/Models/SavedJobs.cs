using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JobseekBerca.Models
{
    public class SavedJobs
    {
        [Key]
        public int savedJobId { get; set; }

        [JsonIgnore]
        public virtual Jobs? Job { get; set; }

        [ForeignKey("Job"), Required]
        public string jobId { get; set; }

        [JsonIgnore]
        public virtual Users? User { get; set; }

        [ForeignKey("User"), Required]
        public string userId { get; set; }
    }
}
