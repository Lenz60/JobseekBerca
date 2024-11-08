using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JobseekBerca.Models
{
    public class Skills
    {
        [Key]
        public int skillId { get; set; }
        public string skillName { get; set; }
        [JsonIgnore]
        public virtual Profiles? Profile { get; set; }
        [ForeignKey("Profile"),Required]
        public string userId { get; set; }
    }
}
