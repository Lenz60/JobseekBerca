using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobseekBerca.Models
{
    public class Certificates
    {
        [Key]
        public int certificateId { get; set; }
        public string title { get; set; }
        public string credentialId { get; set; }
        public string credentialLink { get; set; }
        public string description { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        [JsonIgnore]
        public virtual Profiles? Profile { get; set; }
        [ForeignKey("Profile"), Required]
        public string userId { get; set; }
    }
}
