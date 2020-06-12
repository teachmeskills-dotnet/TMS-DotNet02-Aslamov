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
               .ReverseMap()
               .ForMember(data => data.Value, opt => opt.Ignore());
        }
    }
}