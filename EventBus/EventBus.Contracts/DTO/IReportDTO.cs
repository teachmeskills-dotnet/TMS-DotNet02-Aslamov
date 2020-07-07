using System;

namespace EventBus.Contracts.DTO
{
    /// <summary>
    /// Define interface for data processing report.
    /// </summary>
    public interface IReportDTO
    {
        /// <summary>
        /// Data record identifier.
        /// </summary>
        int RecordId { get; set; }

        /// <summary>
        /// Data acquisition date.
        /// </summary>
        DateTime Date { get; set; }

        /// <summary>
        /// Data type (temperature or acoustic signal).
        /// </summary>
        string DataType { get; set; }

        /// <summary>
        /// Health check of the patient.
        /// </summary>
        string HealthStatus { get; set; }

        /// <summary>
        /// Description of the patient health.
        /// </summary>
        string HealthDescription { get; set; }

        /// <summary>
        /// Recognized patient diseases.
        /// </summary>
        string Diseases { get; set; }

        /// <summary>
        /// Percent accuracy of health assessment.
        /// </summary>
        int Accuracy { get; set; }
    }
}