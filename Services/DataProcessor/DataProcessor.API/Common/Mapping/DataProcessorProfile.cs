using AutoMapper;
using DataProcessor.API.DTO;

namespace DataProcessor.API.Common.Mapping
{
    /// <summary>
    /// Define Automapper profile for DataProcessor.API entities.
    /// </summary>
    public class DataProcessorProfile : Profile
    {
        /// <summary>
        /// Constructor of Automapper profile for DataProcesso.API.
        /// </summary>
        public DataProcessorProfile()
        {
            CreateMap<DataDTO, ReportDTO>()
               .ForMember(report => report.HealthStatus, opt => opt.Ignore())
               .ForMember(report => report.DataType, opt => opt.MapFrom(data => data.SensorDeviceType))
               .ReverseMap()
               .ForMember(data => data.Value, opt => opt.Ignore())
               .ForMember(data => data.SensorDeviceType, opt => opt.MapFrom(report => report.DataType));
        }
    }
}