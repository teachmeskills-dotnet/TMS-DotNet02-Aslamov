using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.API.DTO;
using Identity.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profile.API.Common.Constants;
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

        // GET: api/accounts/{id}
        [HttpGet]
        public async Task<ICollection<AccountDTO>> GetAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            var count = accounts.Count;

            _logger.Information($"{count} {AccountConstants.GET_ACCOUNTS}");

            return await _accountService.GetAllAccountsAsync();
        }

        // GET: api/accounts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _accountService.GetAccountByIdAsync(id);

            if (account == null)
            {
                _logger.Warning($"{account.Username} {AccountConstants.ACCOUNT_NOT_FOUND}");

                return NotFound();
            }

            _logger.Information($"{account.Username} {AccountConstants.GET_FOUND_ACCOUNT}");

            return Ok(account);
        }

        // POST: api/accounts/auth
        [HttpPost("auth")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _accountService.AuthenticateAsync(data);

            if (token == null)
            {
                Log.Warning($"{data.Email} {AccountConstants.EMAIL_NOT_FOUND}");

                return BadRequest(AccountConstants.INCORRECT_USER_LOGIN);
            }

            Log.Information($"{data.Email} {AccountConstants.GET_FOUND_EMAIL}");

            Response.ContentType = "application/json";
            return Accepted(token);
        }

        // POST: api/accounts/registration
        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Register([FromBody] AccountDTO accountDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (success, message) = await _accountService.RegisterAsync(accountDTO);

            if (!success)
            {
                Log.Warning($"{accountDTO.Email} {AccountConstants.ACCOUNT_ALREADY_EXIST}");

                return BadRequest(new { message });
            }

            Log.Information($"{accountDTO.Email} {AccountConstants.REGISTRATION_SUCCESS}");

            return Ok(new { message });
        }

        // PUT: api/accounts/{id}        
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
                Log.Warning($"{accountDTO.Email} {AccountConstants.ACCOUNT_NOT_FOUND}");
                return NotFound();
            }

            var updatedResult = await _accountService.UpdateAccountAsync(accountDTO);

            if (!updatedResult)
            {
                Log.Warning($"{accountDTO.Email} {AccountConstants.UPDATE_ACCOUNT_CONFLICT}");

                return Conflict();
            }

            Log.Information($"{accountDTO.Email} {AccountConstants.UPDATE_ACCOUNT_SUCCESS}");

            return Ok(accountDTO);
        }
    }
}