using AutoMapper;
using Identity.API.Common.Constants;
using Identity.API.Common.Extensions;
using Identity.API.Common.Interfaces;
using Identity.API.Common.Settings;
using Identity.API.DTO;
using Identity.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    /// <summary>
    /// Service to manage user accounts.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IIdentityContext _identityContext;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Constructor of service for managing user accounts.
        /// </summary>
        /// <param name="identityContext">Identity service.</param>
        /// <param name="mapper">Mapping service.</param>
        /// <param name="appSettings">Application settings.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public AccountService( IIdentityContext identityContext,
                               IMapper mapper,
                               IOptions<AppSettings> appSettings)
        {
            _identityContext = identityContext ?? throw new ArgumentNullException(nameof(identityContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
        }

        /// <inheritdoc/>
        public async Task<TokenModel> AuthenticateAsync(LoginDTO loginDTO)
        {
            var account = await _identityContext.Accounts.SingleOrDefaultAsync(x => x.Email == loginDTO.Email && x.Password == loginDTO.Password);

            if (account == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, account.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role.ConvertRole())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtSecurityToken = tokenHandler.WriteToken(token);

            var accountToken = new TokenModel
            {
                Username = account.Username,
                Role = account.Role.ConvertRole(),
                Token = jwtSecurityToken
            };

            return accountToken;
        }

        /// <inheritdoc/>
        public async Task<(bool result, string message)> RegisterAsync(AccountDTO accountDTO)
        {
            var user = await _identityContext.Accounts.FirstOrDefaultAsync(a => a.Email == accountDTO.Email);

            if (user != null)
            {
                return (false, IdentityConstants.USER_ALREADY_EXIST);
            }

            var account = _mapper.Map<AccountDTO, AccountModel>(accountDTO);

            await _identityContext.Accounts.AddAsync(account);
            await _identityContext.SaveChangesAsync(new CancellationToken());

            return (true, IdentityConstants.REGISTRATION_SUCCESS);
        }

        /// <inheritdoc/>
        public async Task<AccountDTO> GetAccountByEmailAsync(string email)
        {
            var account = await _identityContext.Accounts.FirstOrDefaultAsync(p => p.Email == email);
            var accountDTO = _mapper.Map<AccountModel, AccountDTO>(account);

            return accountDTO;
        }

        /// <inheritdoc/>
        public async Task<AccountDTO> GetAccountByIdAsync(Guid accoundId)
        {
            var account = await _identityContext.Accounts.FirstOrDefaultAsync(p => p.Id == accoundId);
            var accountDTO = _mapper.Map<AccountModel, AccountDTO>(account);

            return accountDTO;
        }

        /// <inheritdoc/>
        public async Task<ICollection<AccountDTO>> GetAllAccountsAsync()
        {
            var queriableAccountCollection = _identityContext.Accounts.Select(x => x);

            var collectionOfAccounts = await queriableAccountCollection.ToListAsync();
            var collectionOfaccountDTO = new List<AccountDTO>();

            foreach (var account in collectionOfAccounts)
            {
                var accountDTO = _mapper.Map<AccountDTO>(account);
                collectionOfaccountDTO.Add(accountDTO);
            }

            return collectionOfaccountDTO;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateAccountAsync(AccountDTO accountDTO)
        {
            var account = await _identityContext.Accounts.FirstOrDefaultAsync(p => p.Id == accountDTO.Id);

            if (account == null)
            {
                return false;
            }

            account.Username = accountDTO.Username;
            account.Role = accountDTO.Role;
            account.IsActive = accountDTO.IsActive;
            account.Email = accountDTO.Email;
            account.Password = accountDTO.Password;

            _identityContext.Update(account);
            await _identityContext.SaveChangesAsync(new CancellationToken());

            return true;
        }
    }
}