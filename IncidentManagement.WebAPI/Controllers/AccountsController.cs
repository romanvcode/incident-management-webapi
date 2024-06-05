using IncidentManagement.Core.Models;
using IncidentManagement.Core.ServiceContracts;
using IncidentManagement.Infrastructure.DatabaseContext;
using IncidentManagement.WebAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IncidentManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

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
