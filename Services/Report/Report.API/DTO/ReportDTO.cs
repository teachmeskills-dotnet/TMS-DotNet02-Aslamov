using System;

namespace Report.API.DTO
{
    /// <summary>
    /// Report Data Transfer Object (DTO).
    /// </summary>
    public class ReportDTO
    {
        /// <summary>
        /// User profile identifier.
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Sensor identifier.
        /// </summary>
        public int SensorDeviceId { get; set; }

        /// <summary>
        /// Data acquisition date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Health status of the patient.
        /// </summary>
        public string HealthStatus { get; set; }
    }
}