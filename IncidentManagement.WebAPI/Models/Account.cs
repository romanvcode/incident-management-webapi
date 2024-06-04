using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IncidentManagement.WebAPI.Models
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountName { get; set; }

        [ForeignKey("Incident")]
        public string? IncidentName { get; set; }
        [JsonIgnore]
        public Incident Incident { get; set; } = null!;

        public IEnumerable<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
