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
                Date = DateTime.Now,
                HealthStatus = "Healthy",
                SensorDeviceId = 1,
                SensorDeviceSerial = "123456789",
                SensorDeviceType = "Acoustic",
            };
        }

        /// <summary>
        /// Generate single Data DTO.
        /// </summary>
        /// <returns>Data DTO.</returns>
        public DataDTO GetData()
        {
            return new DataDTO()
            {
                Date = DateTime.Now,
                SensorDeviceId = 1,
                SensorDeviceSerial = "123456789",
                SensorDeviceType = "Acoustic",
                Value = new byte[] { 255, 254, 253, 252 },
            };
        }
    }
}