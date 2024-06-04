using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IncidentManagement.WebAPI.DatabaseContext;
using IncidentManagement.WebAPI.Models;
using IncidentManagement.WebAPI.DTO;

namespace IncidentManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IncidentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateIncident([FromBody] IncidentRequest request)
        {
            // Validate account existence
            var account = await _context.Accounts
                .Include(a => a.Contacts)
                .FirstOrDefaultAsync(a => a.AccountName == request.AccountName);

            if (account == null)
            {
                return NotFound("Account not found");
            }

            // Validate contact existence
            var contact = account.Contacts.FirstOrDefault(c => c.Email == request.ContactEmail);
            if (contact == null)
            {
                contact = new Contact
                {
                    FirstName = request.ContactFirstName,
                    LastName = request.ContactLastName,
                    Email = request.ContactEmail,
                    AccountID = account.AccountID
                };
                _context.Contacts.Add(contact);
            }
            else
            {
                contact.FirstName = request.ContactFirstName;
                contact.LastName = request.ContactLastName;
            }

            // Create new incident
            var incident = new Incident
            {
                Description = request.IncidentDescription,
            };
            _context.Incidents.Add(incident);

            // Link account to the incident
            if (!incident.Accounts.Contains(account))
            {
                account.Incident = incident;
                incident.Accounts.Add(account);
            }

            await _context.SaveChangesAsync();

            return Ok(incident);
        }
    }
}
