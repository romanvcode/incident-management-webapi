using IncidentManagement.Core.Models;
using IncidentManagement.Core.ServiceContracts;
using IncidentManagement.Infrastructure.DatabaseContext;
using IncidentManagement.WebAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IncidentManagement.WebAPI.Controllers
{
    /// <summary>
    /// Controller for account operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountAdderService _accountService;

        /// <summary>
        /// Constructor for the account controller.
        /// </summary>
        /// <param name="accountService"></param>
        public AccountsController(IAccountAdderService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Create a new account (including contact).
        /// </summary>
        /// <param name="request">The account request.</param>
        /// <returns>Returns the account ID and account name.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest request)
        {
            var account = await _accountService.CreateAccount(request);

            if (account == null)
            {
                return Conflict("Contact already exists");
            }

            return Ok(new { account.AccountID, account.AccountName });
        }
    }
}
