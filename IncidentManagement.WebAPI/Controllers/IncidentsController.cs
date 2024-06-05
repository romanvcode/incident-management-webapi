using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IncidentManagement.WebAPI.DatabaseContext;
using IncidentManagement.WebAPI.Models;
using IncidentManagement.WebAPI.DTO;
using IncidentManagement.WebAPI.Helpers;

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

        [HttpGet]
        public async Task<IActionResult> GetIncidents()
        {
            var incidents = await _context.Incidents
                .Include(i => i.Accounts)
                .ThenInclude(a => a.Contacts)
                .Select(i => new
                {
                    i.IncidentName,
                    i.Description,
                    Accounts = i.Accounts.Select(a => new
                    {
                        a.AccountName,
                        Contacts = a.Contacts.Select(c => new
                        {
                            c.FirstName,
                            c.LastName,
                            c.Email
                        })
                    })
                })
                .ToListAsync();

            return Ok(incidents);
        }

        [HttpPost]
        [Route("account")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest request)
        {
            var contact = await _context.Contacts
                .FirstOrDefaultAsync(c => c.Email == request.ContactEmail);

            if (contact != null)
            {
                return Conflict("Contact already exists");
            }

            var account = new Account
            {
                AccountName = request.AccountName
            };

            contact = new Contact
            {
                FirstName = request.ContactFirstName,
                LastName = request.ContactLastName,
                Email = request.ContactEmail,
                Account = account
            };

            account.Contacts.Add(contact);

            _context.Accounts.Add(account);
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return Ok(new { account.AccountID, account.AccountName });
        }

        [HttpPost]
        [Route("incident")]
        public async Task<IActionResult> CreateIncident([FromBody] IncidentRequest request)
        {
            var account = await _context.Accounts
                .Include(a => a.Contacts)
                .FirstOrDefaultAsync(a => a.AccountName == request.AccountName);

            if (account == null)
            {
                return NotFound("Account not found");
            }

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

            var incident = new Incident
            {
                IncidentName = IncidentNameGenerator.GenerateNextKey(_context).Result,
                Description = request.IncidentDescription,
            };

            incident.Accounts.Add(account);
            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();

            return Ok(new { incident.IncidentName, incident.Description });
        }
    }
}
