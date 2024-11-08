using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobseekBerca.Models
{
    public class Certificates
    {
        [Key]
        public string certificateId { get; set; }
        public string certificateName { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        [JsonIgnore]
        public virtual Profiles? Profile { get; set; }
        [ForeignKey("Profile"), Required]
        public string userId { get; set; }
    }
}
