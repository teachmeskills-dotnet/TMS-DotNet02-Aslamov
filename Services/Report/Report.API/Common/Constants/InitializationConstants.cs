namespace Report.API.Common.Constants
{
    /// <summary>
    /// Constants for application services initialization.
    /// </summary>
    public class InitializationConstants
    {
        /// <summary>
        /// Migration error message.
        /// </summary>
        public const string MIGRATION_ERROR = "An error occurred while DB migrating.";

        /// <summary>
        /// Migration success message.
        /// </summary>
        public const string MIGRATION_SUCCESS = "The database is successfully migrated.";

        /// <summary>
        /// Database seed error.
        /// </summary>
        public const string SEED_ERROR = "An error occurred while DB seeding.";

        /// <summary>
        /// Database seed success.
        /// </summary>
        public const string SEED_SUCCEESS = "The database is successfully seeded.";

        /// <summary>
        /// Web host is staring.
        /// </summary>
        public const string WEB_HOST_STARTING = "Web host is starting.";

        /// <summary>
        /// Web host was terminated unexpectedly.
        /// </summary>
        public const string WEB_HOST_TERMINATED = "Web host was terminated unexpectedly!";

        /// <summary>
        /// Web host is stopped successfully.
        /// </summary>
        public const string WEB_HOST_STOPPED = "Web host is stopped successfully.";
    }
}