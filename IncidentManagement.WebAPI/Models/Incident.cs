using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentManagement.WebAPI.Models
{
    public class Incident
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IncidentName { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
