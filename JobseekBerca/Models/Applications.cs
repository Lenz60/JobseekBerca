using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobseekBerca.Models
{
    public class Applications
    {
        [Key]
        public string? applicationId { get; set; }
        public string? status { get; set; }

        [JsonIgnore]
        public virtual Jobs? Jobs { get; set; }
        [ForeignKey("Jobs")]
        public string jobId { get; set; }
        public string userId { get; set; }




    }
}
