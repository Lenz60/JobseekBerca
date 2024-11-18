using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobseekBerca.Models
{
    public enum Status
    {
        Pending,
        Approved,
        Rejected
    }
    public class Applications
    {
        [Key]
        public string? applicationId { get; set; }
        public Status status { get; set; }
        public DateTime? applyDate { get; set; }

        [JsonIgnore]
        public virtual Jobs? Jobs { get; set; }
        [ForeignKey("Jobs")]
        public string jobId { get; set; }
        [JsonIgnore]
        public virtual Users? Users { get; set; }
        [ForeignKey("Users")]
        public string userId { get; set; }
    }
}
