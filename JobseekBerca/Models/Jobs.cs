using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobseekBerca.Models
{
    public class Jobs
    {
        [Key]
        public string? jobId { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? type { get; set; }
        public string? salary { get; set; }
        public string? requirement { get; set; }
        public string? location { get; set; }
        public DateTime? postDate { get; set; }
        public DateTime? dueDate { get; set; }



        [JsonIgnore]
        public virtual Users? Users { get; set; }
        [ForeignKey("Users")]
        public string userId { get; set; }
    }
}
