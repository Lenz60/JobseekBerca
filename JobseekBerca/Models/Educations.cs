using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobseekBerca.Models
{
    public enum Degree
    {
        S1,
        S2,
        S3,
        D3,
        D4,
        D5
    }
    public class Educations
    {
        [Key]
        public int educationId { get; set; }
        public string universityName { get; set; }
        public string programStudy { get; set; }

        public Degree degree { get; set; }
        public string description { get; set; }
        public float gpa { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        [JsonIgnore]
        public virtual Profiles? Profile { get; set; }
        [ForeignKey("Profile"), Required]
        public string userId { get; set; }
    }
}
