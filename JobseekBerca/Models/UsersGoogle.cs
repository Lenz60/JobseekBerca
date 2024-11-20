using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobseekBerca.Models
{
    public class UsersGoogle
    {
        [Key, ForeignKey("Users"), Required]
        public string? userId { get; set; }
        public string? email { get; set; }
        public string? oauthId { get; set; }
        public bool isVerified { get; set; }
        [JsonIgnore]
        public virtual Users? Users { get; set; }
    }
}
