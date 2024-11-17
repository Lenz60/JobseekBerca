using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobseekBerca.Models
{
    public class RefreshToken
    {
        [Key, ForeignKey("Users"), Required]
        public string userId { get; set; }
        public string refreshToken { get; set; }
        [JsonIgnore]
        public virtual Users? Users { get; set; }
    }
}
