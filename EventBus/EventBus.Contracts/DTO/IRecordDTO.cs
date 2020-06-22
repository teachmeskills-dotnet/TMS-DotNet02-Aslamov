using System;

namespace EventBus.Contracts.DTO
{
    /// <summary>
    /// Define interface for record DTO.
    /// </summary>
    public interface IRecordDTO
    {
        /// <summary>
        /// Data identifier.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Data value.
        /// </summary>
        byte[] Value { get; set; }

        /// <summary>
        /// Data acquisition  date.
        /// </summary>
        DateTime Date { get; set; }

        /// <summary>
        /// Mark deleted data.
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Sensor identifier.
        /// </summary>
        int SensorDeviceId { get; set; }

        /// <summary>
        /// Serial of sensor device.
        /// </summary>
        string SensorDeviceSerial { get; set; }
    }
}