using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IncidentManagement.Infrastructure.DatabaseContext;
using IncidentManagement.Core.Models;
using IncidentManagement.WebAPI.DTO;
using IncidentManagement.WebAPI.Helpers;
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

        [HttpGet]
        public async Task<IActionResult> GetIncidents()
        {
            var incidents = await _incidentGetterService.GetIncidents();

            return Ok(incidents);
        }

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
