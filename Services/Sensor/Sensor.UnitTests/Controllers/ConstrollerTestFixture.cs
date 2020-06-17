using Sensor.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sensor.UnitTests.Controllers
{
    /// <summary>
    /// Define base functions for SensorsController testing.
    /// </summary>
    public class ConstrollerTestFixture
    {
        /// <summary>
        /// Generate collection of SensorDTO.
        /// </summary>
        /// <returns>Collection of SensorDTO.</returns>
        public ICollection<SensorDTO> GetAllSensors()
        {
            return new List<SensorDTO>()
            {
                new SensorDTO()
                {
                    Id = 1,
                    Serial = "123456789",
                    SensorType = "Acoustic",
                    SensorTypeId = 1,
                },

                new SensorDTO()
                {
                    Id = 2,
                    Serial = "987654321",
                    SensorType = "Temperature",
                    SensorTypeId = 2,
                },
            };
        }

        /// <summary>
        /// Generate single SensorDTO.
        /// </summary>
        /// <returns>SensorDTO.</returns>
        public SensorDTO GetSensor()
        {
            return new SensorDTO()
            {
                Id = 1,
                Serial = "123456789",
                SensorType = "Acoustic",
                SensorTypeId = 1,
            };
        }

        /// <summary>
        /// Get null sensorDTO.
        /// </summary>
        /// <returns>Null object.</returns>
        public SensorDTO GetNullSensor() => null;

        /// <summary>
        /// Generate collection of RecordDTO.
        /// </summary>
        /// <returns>Collection of RecordDTO.</returns>
        public ICollection<RecordDTO> GetAllRecords()
        {
            var sersorsDTO = GetAllSensors().ToList();

            return new List<RecordDTO>()
            {
                new RecordDTO()
                {
                    IsDeleted = false,
                    Date = DateTime.Parse("01-01-2018"),
                    SensorDeviceId = sersorsDTO.ElementAt(0).Id,
                    SensorDeviceSerial = sersorsDTO.ElementAt(0).Serial,
                    Value = new byte[]{ 255, 254, 253, 252 },
                },

                new RecordDTO()
                {
                    IsDeleted = false,
                    Date = DateTime.Parse("02-01-2018"),
                    SensorDeviceId = sersorsDTO.ElementAt(0).Id,
                    SensorDeviceSerial = sersorsDTO.ElementAt(0).Serial,
                    Value = new byte[] { 0, 1, 2, 3},
                },
            };
        }

        /// <summary>
        /// Generate single RecordDTO.
        /// </summary>
        /// <returns>RecordDTO.</returns>
        public RecordDTO GetRecord()
        {
            var sersorsDTO = GetAllSensors().ToList();

            return new RecordDTO()
            {
                IsDeleted = false,
                Date = DateTime.Parse("02-01-2018"),
                SensorDeviceId = sersorsDTO.ElementAt(0).Id,
                SensorDeviceSerial = sersorsDTO.ElementAt(0).Serial,
                Value = new byte[] { 0, 1, 2, 3 },
            };
        }

        /// <summary>
        /// Get null recordDTO.
        /// </summary>
        /// <returns>Null object.</returns>
        public RecordDTO GetNullRecord() => null;
    }
}