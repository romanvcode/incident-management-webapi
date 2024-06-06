using System.ComponentModel.DataAnnotations;

namespace IncidentManagement.Core.Models
{
    public class Incident
    {
        [Key]
        public string? IncidentName { get; set; }

        [Required]
        [StringLength(50)]
        public string? Description { get; set; }

        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
