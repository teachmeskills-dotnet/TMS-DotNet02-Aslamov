using System;
namespace Identity.API.Models
{
    /// <summary>
    /// User account entity.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// GUID Identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User role in application.
        /// </summary>
        public int Role { get; set; }

        /// <summary>
        /// Is Active account.
        /// </summary>
        public bool IsActive { get; set; }
    }
}