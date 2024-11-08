using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JobseekBerca.Models
{
    public enum Gender
    {
        Laki_laki,
        Perempuan
    }
    public class Profiles
    {
        [Key, ForeignKey("Users")]
        public string userId { get; set; }
        public string fullName { get; set; }
        public string? summary { get; set; }
        public Gender? gender { get; set; }
        public string? address { get; set; }
        public DateTime? birthdate { get; set; }
        [JsonIgnore]
        public virtual Users? Users { get; set; }

    }
}
