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
        public string Email { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        public string Password { get; set; }
    }
}