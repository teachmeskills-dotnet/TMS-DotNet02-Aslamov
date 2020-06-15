namespace Identity.API.Models
{
    /// <summary>
    /// User authentication token model.
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// User name.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User role in application.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Authentication token.
        /// </summary>
        public string Token { get; set; }
    }
}