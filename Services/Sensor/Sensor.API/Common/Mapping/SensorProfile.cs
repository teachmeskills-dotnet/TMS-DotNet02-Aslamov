using AutoMapper;
using Sensor.API.DTO;
using Sensor.API.Models;

namespace Sensor.API.Common.Mapping
{
    /// <summary>
    /// Define Automapper profile for Sensor.API entities.
    /// </summary>
    public class SensorProfile : Profile
    {
        /// <summary>
        /// Constructor of Automapper profile for Sensor.API.
        /// </summary>
        public SensorProfile()
        {
            CreateMap<SensorDevice, SensorDTO>()
                .ForMember(dto => dto.SensorType, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(s => s.SensorType, opt => opt.Ignore());

            CreateMap<SensorRecord, RecordDTO>()
                .ForMember(dto => dto.SensorDeviceSerial, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(r => r.SensorDevice, opt => opt.Ignore());
        }
    }
}