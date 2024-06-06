using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentManagement.Core.Models
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        [Required]
        [StringLength(50)]
        public string? AccountName { get; set; }

        [ForeignKey("Incident")]
        public string? IncidentName { get; set; }
        public Incident Incident { get; set; } = null!;

        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
