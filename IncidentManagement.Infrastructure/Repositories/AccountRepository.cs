using IncidentManagement.Core.Models;
using IncidentManagement.Core.RepositoryContracts;
using IncidentManagement.Infrastructure.DatabaseContext;
using IncidentManagement.WebAPI.DTO;
using Microsoft.EntityFrameworkCore;

namespace IncidentManagement.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Account?> CreateAccount(AccountRequest request)
        {
            var contact = await _context.Contacts
                .FirstOrDefaultAsync(c => c.Email == request.ContactEmail);

            if (contact != null)
            {
                return null;
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

            return account;
        }
    }
}
