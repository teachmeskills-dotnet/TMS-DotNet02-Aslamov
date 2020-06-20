using System.ComponentModel.DataAnnotations;

namespace Identity.API.DTO
{
    /// <summary>
    /// Login Data Transfer Object.
    /// </summary>
    public class LoginDTO
    {
        /// <summary>
        /// User email.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}