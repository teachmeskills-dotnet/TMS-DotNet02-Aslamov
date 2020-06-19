using System;
using System.ComponentModel.DataAnnotations;

namespace Identity.API.DTO
{
    /// <summary>
    /// Account Data Transfer Object (DTO).
    /// </summary>
    public class AccountDTO
    {
        /// <summary>
        /// GUID Identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User Email.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// User Password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Username.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// User role in application.
        /// </summary>
        [Required]
        public int Role { get; set; }

        /// <summary>
        /// Is Active account.
        /// </summary>
        [Required]
        public bool IsActive { get; set; }
    }
}