using IncidentManagement.Core.Models;
using IncidentManagement.Core.RepositoryContracts;
using IncidentManagement.Core.ServiceContracts;
using IncidentManagement.WebAPI.DTO;

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

        /// <summary>
        /// Create Account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Account?> CreateAccount(AccountRequest request)
        {
            var account = await _accountRepository.CreateAccount(request);

            return account;
        }
    }
}
