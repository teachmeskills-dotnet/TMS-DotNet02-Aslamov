using Report.API.DTO;
using Report.API.Models;

namespace Report.API.Common.Mapping
{
    /// <summary>
    /// Define Automapper profile for Report.API entities.
    /// </summary>
    public class MappingProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Constructor of Automapper profile for Report.API.
        /// </summary>
        public MappingProfile() => CreateMap<ReportModel, ReportDTO>().ReverseMap();
    }
}