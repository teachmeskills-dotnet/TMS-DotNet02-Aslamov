using System;
namespace DataProcessor.API.DTO
{
    /// <summary>
    /// Data transfer object of processing report.
    /// </summary>
    public class ReportDTO
    {
        /// <summary>
        /// Report Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Data acquisition  date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Sensor identifier.
        /// </summary>
        public int SensorDeviceId { get; set; }

        /// <summary>
        /// Serial of sensor device.
        /// </summary>
        public string SensorDeviceSerial { get; set; }

        /// <summary>
        /// Type of sensor device.
        /// </summary>
        public string SensorDeviceType { get; set; }

        /// <summary>
        /// Health status of the patient.
        /// </summary>
        public string HealthStatus { get; set; }

    }
}
