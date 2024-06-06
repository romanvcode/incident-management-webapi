using IncidentManagement.Core.Models;
using IncidentManagement.WebAPI.DTO;

namespace IncidentManagement.Core.ServiceContracts
{
    /// <summary>
    /// Interface for the Incident Service
    /// </summary>
    public interface IIncidentAdderService
    {
        /// <summary>
        /// Create an incident
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Incident?> CreateIncident(IncidentRequest request);
    }
}
