using IncidentManagement.Core.Models;
using IncidentManagement.Core.RepositoryContracts;
using IncidentManagement.Core.ServiceContracts;
using IncidentManagement.WebAPI.DTO;

namespace IncidentManagement.Core.Services
{
    /// <summary>
    /// Incident Service
    /// </summary>
    public class IncidentAdderService : IIncidentAdderService
    {
        private readonly IIncidentRepository _incidentRepository;

        public IncidentAdderService(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }

        /// <summary>
        /// Create an incident
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Incident?> CreateIncident(IncidentRequest request)
        {
            var incident = await _incidentRepository.CreateIncident(request);

            return incident;
        }
    }
}
