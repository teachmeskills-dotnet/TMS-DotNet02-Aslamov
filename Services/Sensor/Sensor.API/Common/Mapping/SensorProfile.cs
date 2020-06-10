using AutoMapper;
using Sensor.API.DTO;
using Sensor.API.Models;

namespace Sensor.API.Common.Mapping
{
    /// <summary>
    /// Define Automapper profile for sensor.API entities.
    /// </summary>
    public class SensorProfile : Profile
    {
        /// <summary>
        /// Automapper profile constructor.
        /// </summary>
        public SensorProfile()
        {
            CreateMap<SensorDevice, SensorDTO>().ReverseMap();
            CreateMap<SensorRecord, RecordDTO>().ReverseMap();
        }
    }
}