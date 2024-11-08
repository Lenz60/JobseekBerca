using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JobseekBerca.Models
{
    public class Users
    {
        [Key]
        public string? userId { get; set; }     
        public string? password { get; set; }        
        public string? email { get; set; }
        [JsonIgnore]
        public virtual Roles? Roles { get; set; }
        [ForeignKey("Roles")]
        public string roleId { get; set; }
    }
}
