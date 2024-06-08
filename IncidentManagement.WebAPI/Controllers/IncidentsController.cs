using Microsoft.AspNetCore.Mvc;
using IncidentManagement.WebAPI.DTO;
using IncidentManagement.Core.ServiceContracts;

namespace IncidentManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentsController : ControllerBase
    {
        private readonly IIncidentAdderService _incidentAdderService;
        private readonly IIncidentGetterService _incidentGetterService;

        public IncidentsController(IIncidentAdderService incidentAdderService, IIncidentGetterService incidentGetterService)
        {
            _incidentAdderService = incidentAdderService;
            _incidentGetterService = incidentGetterService;
        }

        /// <summary>
        /// Get all incidents (including account information).
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetIncidents()
        {
            var incidents = await _incidentGetterService.GetIncidents();

            return Ok(incidents);
        }

        /// <summary>
        /// Create a new incident.
        /// </summary>
        /// <param name="request">Incident request.</param>
        /// <returns>Returns the incident name and description.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateIncident([FromBody] IncidentRequest request)
        {
            var incident = await _incidentAdderService.CreateIncident(request);

            if (incident == null)
            {
                return NotFound("Account not found");
            }

            return Ok(new { incident.IncidentName, incident.Description });
        }
    }
}
