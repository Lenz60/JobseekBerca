using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JobseekBerca.Models
{
    public class Roles
    {
        [Key]
        public string? roleId { get; set; }
        public string? roleName { get; set; }

    }
}
