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
        private readonly IIncidentService _incidentService;

        public IncidentsController(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetIncidents()
        {
            var incidents = await _incidentService.GetIncidents();

            return Ok(incidents);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncident([FromBody] IncidentRequest request)
        {
            var incident = await _incidentService.CreateIncident(request);

            if (incident == null)
            {
                return NotFound("Account not found");
            }

            return Ok(new { incident.IncidentName, incident.Description });
        }
    }
}
