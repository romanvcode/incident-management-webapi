using IncidentManagement.Core.RepositoryContracts;
using IncidentManagement.Core.ServiceContracts;

namespace IncidentManagement.Core.Services
{
    /// <summary>
    /// Incident Service
    /// </summary>
    public class IncidentGetterService : IIncidentGetterService
    {
        private readonly IIncidentRepository _incidentRepository;

        public IncidentGetterService(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }

        /// <summary>
        /// Get all incidents
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<object>> GetIncidents()
        {
            var incidents = await _incidentRepository.GetIncidents();

            return incidents;
        }
    }
}
