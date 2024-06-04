﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IncidentManagement.WebAPI.Models
{
    public class Incident
    {
        [Key]
        public string IncidentName { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
