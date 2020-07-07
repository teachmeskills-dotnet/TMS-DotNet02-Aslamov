using AutoMapper;
using DataProcessor.API.DTO;
using EventBus.Contracts.DTO;

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
            CreateMap<IDataDTO, ReportDTO>()
               .ForMember(report => report.HealthStatus, opt => opt.Ignore())
               .ForMember(report => report.DataType, opt => opt.MapFrom(data => data.SensorDeviceType))
               .ReverseMap()
               .ForMember(data => data.Value, opt => opt.Ignore())
               .ForMember(data => data.SensorDeviceType, opt => opt.MapFrom(report => report.DataType));

            CreateMap<IRecordDTO, ReportDTO>()
               .ForMember(report => report.HealthStatus, opt => opt.Ignore())
               .ForMember(report => report.RecordId, opt => opt.MapFrom(data => data.Id))
               .ForMember(report => report.DataType, opt => opt.MapFrom(data => data.SensorDeviceType))
               .ReverseMap()
               .ForMember(data => data.Value, opt => opt.Ignore())
               .ForMember(data => data.SensorDeviceType, opt => opt.MapFrom(report => report.DataType))
               .ForMember(data => data.Id, opt => opt.MapFrom(report => report.RecordId))
               .ForMember(data => data.IsDeleted, opt => opt.Ignore());
        }
    }
}