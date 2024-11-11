using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobseekBerca.Models
{
    public class SocialMedias
    {
        [Key]
        public int socialMediaId { get; set; }
        public string socialMediaName { get; set; }
        public string socialMediaLink { get; set; }
        [JsonIgnore]
        public virtual Profiles? Profile { get; set; }
        [ForeignKey("Profile"), Required]
        public string userId { get; set; }
    }
}
