using System;

namespace EventBus.Contracts.DTO
{
    /// <summary>
    /// Define interface for data DTO.
    /// </summary>
    public interface IDataDTO
    {
        /// <summary>
        /// Data value.
        /// </summary>
        byte[] Value { get; set; }

        /// <summary>
        /// Data acquisition  date.
        /// </summary>
        DateTime Date { get; set; }

        /// <summary>
        /// Sensor identifier.
        /// </summary>
        int SensorDeviceId { get; set; }

        /// <summary>
        /// Serial of sensor device.
        /// </summary>
        string SensorDeviceSerial { get; set; }

        /// <summary>
        /// Type of sensor device.
        /// </summary>
        public string SensorDeviceType { get; set; }
    }
}