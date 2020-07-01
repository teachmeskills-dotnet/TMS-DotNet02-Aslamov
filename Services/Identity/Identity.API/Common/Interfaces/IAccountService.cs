using Identity.API.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.API.Common.Interfaces
{
    /// <summary>
    /// Interface for service to manage user accounts.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// User login.
        /// </summary>
        /// <param name="loginDTO">User login data.</param>
        /// <returns>Application Token.</returns>
        Task<TokenDTO> LoginAsync(LoginDTO loginDTO);

        /// <summary>
        /// User registration.
        /// </summary>
        /// <param name="accountDTO">User account data.</param>
        /// <returns>Uperation result.</returns>
        Task<(Guid id, bool result, string message)> RegisterAsync(AccountDTO accountDTO);

        /// <summary>
        /// Get account by email address.
        /// </summary>
        /// <param name="email">Email address.</param>
        /// <returns>Account DTO.</returns>
        Task<AccountDTO> GetAccountByEmailAsync(string email);

        /// <summary>
        /// Get user account by user Id.
        /// </summary>
        /// <param name="accoundId">Account Identifier (GUID).</param>
        /// <returns>Account DTO.</returns>
        Task<AccountDTO> GetAccountByIdAsync(Guid accoundId);

        /// <summary>
        /// Get account by username.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns>Account DTO.</returns>
        Task<AccountDTO> GetAccountByUsernameAsync(string username);

        /// <summary>
        /// Get all user accounts.
        /// </summary>
        /// <returns>Collection of user account DTO.</returns>
        Task<ICollection<AccountDTO>> GetAllAccountsAsync();

        /// <summary>
        /// Update user account.
        /// </summary>
        /// <param name="accountDTO">User account DTO.</param>
        /// <returns>Operation result.</returns>
        Task<bool> UpdateAccountAsync(AccountDTO accountDTO);

        /// <summary>
        /// Delete user account.
        /// </summary>
        /// <param name="accountId">Account identifier.</param>
        /// <returns>Operation result.</returns>
        Task<bool> DeleteAccountByIdAsync(Guid accountId);
    }
}