using IncidentManagement.Core.Models;
using IncidentManagement.Core.RepositoryContracts;
using IncidentManagement.Core.ServiceContracts;
using IncidentManagement.WebAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace IncidentManagement.Core.Services
{
    /// <summary>
    /// Incident Service
    /// </summary>
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _incidentRepository;

        public IncidentService(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }

        public async Task<IEnumerable<object>> GetIncidents()
        {
            var incidents = await _incidentRepository.GetIncidents();

            return incidents;
        }

        public async Task<Incident?> CreateIncident(IncidentRequest request)
        {
            var incident = await _incidentRepository.CreateIncident(request);

            return incident;
        }
    }
}
