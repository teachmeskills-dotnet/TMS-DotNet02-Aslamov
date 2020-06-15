namespace Identity.API.Models
{
    /// <summary>
    /// Entity for login .
    /// </summary>
    public class Login
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