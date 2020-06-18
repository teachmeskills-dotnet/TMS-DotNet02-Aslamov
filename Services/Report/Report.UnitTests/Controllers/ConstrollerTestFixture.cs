using Report.API.DTO;
using System;
using System.Collections.Generic;

namespace Report.UnitTests.Controllers
{
    /// <summary>
    /// Define base functions for ReportsController testing.
    /// </summary>
    public class ConstrollerTestFixture
    {
        /// <summary>
        /// Generate collection of ReportDTO.
        /// </summary>
        /// <returns>Collection of ReportDTO.</returns>
        public ICollection<ReportDTO> GetAllReports()
        {
            return new List<ReportDTO>()
            {
               new ReportDTO()
                {
                    Id = 1,
                    Date = DateTime.Parse("01-01-2020"),
                    SensorDeviceId = 1,
                    HealthStatus = "Healthy",
                    HealthDescription = "You are totally healthy",
                    DataType = "Temperature",
                    Diseases = string.Empty,
                    Accuracy = 97,
                },

                new ReportDTO()
                {
                    Id = 2,
                    Date = DateTime.Parse("01-02-2020"),
                    SensorDeviceId = 2,
                    HealthStatus = "Diseased",
                    HealthDescription = "Several health problems have been recognized...",
                    DataType = "Acoustic",
                    Diseases = "Mitral valve prolapse",
                    Accuracy = 76,
                },
            };
        }

        /// <summary>
        /// Generate single ReportDTO.
        /// </summary>
        /// <returns>ReportDTO.</returns>
        public ReportDTO GetReport()
        {
            return new ReportDTO()
            {
                Id = 1,
                Date = DateTime.Parse("01-01-2020"),
                SensorDeviceId = 1,
                HealthStatus = "Healthy",
                HealthDescription = "You are totally healthy",
                DataType = "Temperature",
                Diseases = string.Empty,
                Accuracy = 97,
            };
        }

        /// <summary>
        /// Get null ReportDTO.
        /// </summary>
        /// <returns>Null object.</returns>
        public ReportDTO GetNullReport() => null;
    }
}