using EventBus.Contracts.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace Report.API.DTO
{
    /// <summary>
    /// Report Data Transfer Object (DTO).
    /// </summary>
    public class ReportDTO : IReportDTO
    {
        /// <summary>
        /// Report Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Sensor identifier.
        /// </summary>
        [Required]
        public int SensorDeviceId { get; set; }

        /// <summary>
        /// Data acquisition date.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Data type (temperature or acoustic signal).
        /// </summary>
        [Required]
        public string DataType { get; set; }

        /// <summary>
        /// Health check of the patient.
        /// </summary>
        [Required]
        public string HealthStatus { get; set; }

        /// <summary>
        /// Description of the patient health.
        /// </summary>
        [Required]
        public string HealthDescription { get; set; }

        /// <summary>
        /// Recognized patient diseases.
        /// </summary>
        public string Diseases { get; set; }

        /// <summary>
        /// Percent accuracy of health assessment.
        /// </summary>
        [Required]
        public int Accuracy { get; set; }
    }
}