using DataProcessor.API.Common.Constants;
using DataProcessor.API.Common.Enums;
using System.Collections.Generic;
using System.Linq;

namespace DataProcessor.API.Common.Dictionaries
{
    /// <summary>
    /// Information dictionary for dataProcessor.
    /// </summary>
    public class DataProcessorDictionary
    {
        private static Dictionary<HealthStatus, string> _healthDescriptionDictionary = new Dictionary<HealthStatus, string>()
            {
                { HealthStatus.Unknown, HealthDescriptionConstants.UNKNOWN_DESCRIPTION },
                { HealthStatus.Healthy, HealthDescriptionConstants.HEALTY_DESCRIPTION},
                { HealthStatus.Diseased, HealthDescriptionConstants.DISEASED_DESCRIPTION },
            };

        private static List<string> _heartDiseases = new List<string>()
        {
            HeartDiseasesConstants.ARRHYTHMIA,
            HeartDiseasesConstants.CORONARY_ARTERY_DISEASE,
            HeartDiseasesConstants.DILATED_CARDIOMYOPATHY,
            HeartDiseasesConstants.MYOCARDIAL_INFARCTION,
            HeartDiseasesConstants.HEART_FAILURE,
            HeartDiseasesConstants.HYPERTROPHIC_CARDIOMYOPATHY,
            HeartDiseasesConstants.MITRAL_REGURGITATION,
            HeartDiseasesConstants.MITRAL_VALVE_PROLAPSE,
            HeartDiseasesConstants.PULMONARY_STENOSIS,
        };

        private static List<string> _commonDesiases = new List<string>()
        {
            CommonDiseasesConstants.COMMON_COLD,
            CommonDiseasesConstants.FEVER,
        };

        /// <summary>
        /// Get health description for sertain health status.
        /// </summary>
        /// <param name="healthStatus">Health status (Enum).</param>
        /// <returns>Description for health status.</returns>
        public static string GetHealthDescription(HealthStatus healthStatus) => _healthDescriptionDictionary.GetValueOrDefault(healthStatus);

        /// <summary>
        /// Get heart disease by idintifier.
        /// </summary>
        /// <param name="id">Heart disease identifier.</param>
        /// <returns>Heart disease.</returns>
        public static string GetHeartDisease(int id) => _heartDiseases.ElementAtOrDefault(id);

        /// <summary>
        /// Get count of heart diseases to been recognized.
        /// </summary>
        /// <returns>Count of heart diseases.</returns>
        public static int GetHeartDiseasesCount() => _heartDiseases.Count;

        /// <summary>
        /// Get common disease by idintifier.
        /// </summary>
        /// <param name="id">Common disease identifier.</param>
        /// <returns>Common disease.</returns>
        public static string GetCommonDisease(int id) => _commonDesiases.ElementAtOrDefault(id);

        /// <summary>
        /// Get count of common diseases to been recognized.
        /// </summary>
        /// <returns>Count of common diseases.</returns>
        public static int GetCommonDiseasesCount() => _commonDesiases.Count;
    }
}