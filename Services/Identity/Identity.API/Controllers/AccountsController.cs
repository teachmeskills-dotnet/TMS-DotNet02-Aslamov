using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.API.Common.Interfaces;
using Identity.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Identity.API.Common.Constants;
using Serilog;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor of controller for account managment.
        /// </summary>
        /// <param name="accountService">Account management service.</param>
        /// <param name="logger">Logging service.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public AccountsController(IAccountService accountService,
                                  ILogger logger)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/accounts
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ICollection<AccountDTO>> GetAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            var count = accounts.Count;

            _logger.Information($"{count} {AccountConstants.GET_ACCOUNTS}");
            return await _accountService.GetAllAccountsAsync();
        }

        // GET: api/accounts/{id}
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAccount([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null)
            {
                _logger.Warning($"{id} {AccountConstants.ACCOUNT_NOT_FOUND}");
                return NotFound(id);
            }

            _logger.Information($"{account.Username} {AccountConstants.GET_FOUND_ACCOUNT}");
            return Ok(account);
        }

        // GET: api/accounts/{username}
        [Authorize(Roles = "User,Admin")]
        [HttpGet("{username}")]
        public async Task<IActionResult> GetAccount([FromRoute] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _accountService.GetAccountByUsernameAsync(username);
            if (account == null)
            {
                _logger.Warning($"{username} {AccountConstants.ACCOUNT_NOT_FOUND}");
                return NotFound(username);
            }

            _logger.Information($"{account.Username} {AccountConstants.GET_FOUND_ACCOUNT}");
            return Ok(account);
        }

        // POST: api/accounts/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _accountService.LoginAsync(data);
            if (token == null)
            {
                _logger.Warning($"{data.Email} {AccountConstants.ACCOUNT_NOT_FOUND}");
                return NotFound(new { Message = AccountConstants.INCORRECT_USER_LOGIN });
            }

            _logger.Information($"{data.Email} {AccountConstants.GET_FOUND_ACCOUNT}");

            Response.ContentType = "application/json";
            return Accepted(token);
        }

        // POST: api/accounts/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewAccount([FromBody] AccountDTO accountDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (id, success, message) = await _accountService.RegisterAsync(accountDTO);
            if (!success)
            {
                _logger.Warning($"{accountDTO.Email} {AccountConstants.ACCOUNT_ALREADY_EXIST}");
                return Conflict(new { message });
            }

            _logger.Information($"{accountDTO.Email} {AccountConstants.REGISTRATION_SUCCESS}");
            accountDTO.Id = id;
            return CreatedAtAction(nameof(RegisterNewAccount), accountDTO);
        }

        // PUT: api/accounts/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount([FromBody] AccountDTO accountDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _accountService.GetAccountByEmailAsync(accountDTO.Email);
            if (account == null)
            {
                _logger.Warning($"{accountDTO.Email} {AccountConstants.ACCOUNT_NOT_FOUND}");
                return NotFound(accountDTO.Id);
            }

            var success = await _accountService.UpdateAccountAsync(accountDTO);
            if (!success)
            {
                _logger.Warning($"{accountDTO.Email} {AccountConstants.UPDATE_ACCOUNT_CONFLICT}");
                return Conflict();
            }

            _logger.Information($"{accountDTO.Email} {AccountConstants.UPDATE_ACCOUNT_SUCCESS}");
            return Ok(accountDTO);
        }

        // DELETE: api/accounts/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _accountService.DeleteAccountByIdAsync(id);
            if (!success)
            {
                _logger.Warning($"{id} {AccountConstants.ACCOUNT_NOT_FOUND}");
                return NotFound(id);
            }

            _logger.Information($"{id} {AccountConstants.DELETE_ACCOUNT_SUCCESS}");
            return Ok(id);
        }
    }
}