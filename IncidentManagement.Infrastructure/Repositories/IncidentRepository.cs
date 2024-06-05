using Azure.Core;
using IncidentManagement.Core.Models;
using IncidentManagement.Core.RepositoryContracts;
using IncidentManagement.Infrastructure.DatabaseContext;
using IncidentManagement.WebAPI.DTO;
using IncidentManagement.WebAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IncidentManagement.Infrastructure.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly ApplicationDbContext _context;

        public IncidentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetIncidents()
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

            return incidents;
        }

        public async Task<Incident?> CreateIncident(IncidentRequest request)
        {
            var account = await _context.Accounts
                .Include(a => a.Contacts)
                .FirstOrDefaultAsync(a => a.AccountName == request.AccountName);

            if (account == null)
            {
                return null;
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

            return incident;
        }
    }
}
