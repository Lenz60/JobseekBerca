using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JobseekBerca.Models
{
    public class Experiences
    {
        [Key]
        public int experienceId { get; set; }
        public string? position { get; set; }
        public string? company { get; set; }
        public string? description { get; set; }
        public string? jobTypes { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        [JsonIgnore]
        public virtual Profiles? Profile { get; set; }
        [ForeignKey("Profile"), Required]
        public string userId { get; set; }
    }
}
