using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JobseekBerca.Models
{
    public enum Gender
    {
        Male,
        Female
    }
    public class Profiles
    {
        [JsonIgnore]
        public virtual Users? Users { get; set; }
        [Key, ForeignKey("Users"), Required]
        public string userId { get; set; }
        public string? fullName { get; set; }
        public string? summary { get; set; }
        public Gender? gender { get; set; }
        public string? address { get; set; }
        public string? phoneNumber { get; set; }
        public DateTime? birthDate { get; set; }
        public string? profileImage { get; set; }
        public string? linkPersonalWebsite { get; set; }
        public string? linkGithub { get; set; }
        public virtual ICollection<Experiences> Experiences { get; set; } = new List<Experiences>();
        public virtual ICollection<Educations> Educations { get; set; } = new List<Educations>();
        public virtual ICollection<Skills> Skills { get; set; } = new List<Skills>();



    }
}
