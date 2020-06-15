namespace Identity.API.Common.Settings
{
    /// <summary>
    /// Application settings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Secret key.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Application environment (OS/Docker):
        /// false - OS,
        /// true - Docker.
        /// </summary>
        public bool IsProduction { get; set; }
    }
}