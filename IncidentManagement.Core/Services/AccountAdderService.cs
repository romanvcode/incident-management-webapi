using IncidentManagement.Core.Models;
using IncidentManagement.Core.RepositoryContracts;
using IncidentManagement.Core.ServiceContracts;
using IncidentManagement.WebAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentManagement.Core.Services
{
    /// <summary>
    /// Account Service
    /// </summary>
    public class AccountAdderService : IAccountAdderService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountAdderService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account?> CreateAccount(AccountRequest request)
        {
            var account = await _accountRepository.CreateAccount(request);

            return account;
        }
    }
}
