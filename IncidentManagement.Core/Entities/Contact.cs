using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentManagement.Core.Models
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
        public Account Account { get; set; } = null!;
    }
}
