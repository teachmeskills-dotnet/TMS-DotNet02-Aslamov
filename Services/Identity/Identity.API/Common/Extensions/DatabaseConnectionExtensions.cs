namespace Identity.API.Common.Extensions
{
    /// <summary>
    /// Extension to extract the proper DB connection string based on current environment.
    /// </summary>
    public static class DatabaseConnectionExtensions
    {
        /// <summary>
        /// Choose connection string base on the currenty environment.
        /// </summary>
        /// <param name="environment">Applicaion environment.</param>
        /// <returns>Connection string.</returns>
        public static string ToDbConnectionString(this bool environment)
        {
            string result = environment switch
            {
                true => "DockerConnection",
                _ => "DefaultConnection",
            };
            return result;
        }
    }
}