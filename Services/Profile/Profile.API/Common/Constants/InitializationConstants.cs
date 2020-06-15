namespace Profile.API.Common.Constants
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
    }
}