﻿using System;

namespace Report.API.DTO
{
    /// <summary>
    /// Report Data Transfer Object (DTO).
    /// </summary>
    public class ReportDTO
    {
        /// <summary>
        /// Sensor identifier.
        /// </summary>
        public int SensorDeviceId { get; set; }

        /// <summary>
        /// Data acquisition date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Data type (temperature or acoustic signal).
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// Health check of the patient.
        /// </summary>
        public string HealthStatus { get; set; }

        /// <summary>
        /// Description of the patient health.
        /// </summary>
        public string HealthDescription { get; set; }

        /// <summary>
        /// Recognized patient diseases.
        /// </summary>
        public string Diseases { get; set; }

        /// <summary>
        /// Percent accuracy of health assessment.
        /// </summary>
        public int Accuracy { get; set; }
    }
}