using Sensor.API.DTO;
using System.Collections.Generic;

namespace Sensor.UnitTests.Controllers
{
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
        /// <returns></returns>
        public SensorDTO GetNullSensor() => null;
    }
}