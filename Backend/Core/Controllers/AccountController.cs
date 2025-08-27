using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("GetAllAccounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            var response = await _accountService.GetAllAccountsAsync();
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetAccountById")]
        public async Task<IActionResult> GetAccountById([FromQuery] int id)
        {
            var response = await _accountService.GetAccountByIdAsync(id);
            if (!response.Succeeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] Account account)
        {
            if (account == null)
            {
                return BadRequest("Account cannot be null.");
            }
            var response = await _accountService.CreateAccountAsync(account);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return CreatedAtAction(nameof(GetAccountById), new { id = response.Data.AccountId }, response);
        }

        [HttpPut("UpdateAccount")]
        public async Task<IActionResult> UpdateAccount([FromBody] Account account)
        {
            if (account == null || account.AccountId <= 0)
            {
                return BadRequest("Invalid account data.");
            }
            var response = await _accountService.UpdateAccountAsync(account);
            if (!response.Succeeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid account ID.");
            }
            var response = await _accountService.DeleteAccountAsync(id);
            if (!response.Succeeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
