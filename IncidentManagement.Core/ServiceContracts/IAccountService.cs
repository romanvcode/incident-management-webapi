using IncidentManagement.Core.Models;
using IncidentManagement.WebAPI.DTO;

namespace IncidentManagement.Core.ServiceContracts
{
    /// <summary>
    /// Interface for the Account Service
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Create an account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Account?> CreateAccount(AccountRequest request);
    }
}
