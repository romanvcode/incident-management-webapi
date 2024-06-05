using IncidentManagement.Core.Models;
using IncidentManagement.WebAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace IncidentManagement.Core.ServiceContracts
{
    /// <summary>
    /// Interface for the Incident Service
    /// </summary>
    public interface IIncidentService
    {
        /// <summary>
        /// Get all incidents
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<object>> GetIncidents();

        /// <summary>
        /// Create an incident
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Incident?> CreateIncident(IncidentRequest request);
    }
}
