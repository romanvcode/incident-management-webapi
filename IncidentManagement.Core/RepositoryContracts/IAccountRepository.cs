using IncidentManagement.Core.Models;
using IncidentManagement.WebAPI.DTO;

namespace IncidentManagement.Core.RepositoryContracts
{
    /// <summary>
    /// Interface for the Account Repository
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// /// Create an account
        /// /// </summary>
        /// /// <param name="request"></param>
        /// /// <returns></returns>
        Task<Account?> CreateAccount(AccountRequest request);
    }
}
