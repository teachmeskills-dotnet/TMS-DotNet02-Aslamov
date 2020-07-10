namespace DataProcessor.API.Common.Constants
{
    /// <summary>
    /// Define constanst for patient health description.
    /// </summary>
    public class HealthDescriptionConstants
    {
        /// <summary>
        /// There are some problems with data processing.
        /// </summary>
        public const string UNKNOWN_DESCRIPTION = "There are some problems with data processing... Try again later, please.";

        /// <summary>
        /// No health problem have been redognized.
        /// </summary>
        public const string HEALTY_DESCRIPTION = "No health problems have been recognized. You are totally healthy!";

        /// <summary>
        /// Several health problems have been recognized
        /// </summary>
        public const string DISEASED_DESCRIPTION = "Several health problems have been recognized...";
    }
}