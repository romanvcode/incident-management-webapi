using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IncidentManagement.WebAPI.Models
{
    public class Contact
    {
        [Key]
        public int ContactID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [ForeignKey("Account")]
        public int AccountID { get; set; }
        [JsonIgnore]
        public Account Account { get; set; } = null!;
    }
}
