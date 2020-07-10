using DataProcessor.API.Common.Constants;
using DataProcessor.API.DTO;
using System;

namespace DataProcessor.UnitTests.Controllers
{
    /// <summary>
    /// Define base functions for controller tests.
    /// </summary>
    public class ControllerTestFixture
    {
        /// <summary>
        /// Generate single Report DTO.
        /// </summary>
        /// <returns>Report DTO.</returns>
        public ReportDTO GetReport()
        {
            return new ReportDTO()
            {
                RecordId = 1,
                Date = DateTime.Now,
                HealthStatus = "Healthy",
                DataType = "Acoustic",
                Accuracy = 100,
                Diseases = string.Empty,
                HealthDescription = HealthDescriptionConstants.HEALTY_DESCRIPTION,
            };
        }

        /// <summary>
        /// Generate single Data DTO.
        /// </summary>
        /// <returns>Data DTO.</returns>
        public RecordDTO GetData()
        {
            return new RecordDTO()
            {
                Id = 1,
                Date = DateTime.Now,
                SensorDeviceId = 1,
                SensorDeviceSerial = "123456789",
                SensorDeviceType = "Acoustic",
                Value = new byte[] { 255, 254, 253, 252 },
            };
        }
    }
}