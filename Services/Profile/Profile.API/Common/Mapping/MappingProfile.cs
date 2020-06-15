using Profile.API.DTO;
using Profile.API.Models;

namespace Profile.API.Common.Mapping
{
    /// <summary>
    /// Define Automapper profile for Profile.API entities.
    /// </summary>
    public class MappingProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Constructor of Automapper profile for Profile.API.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<ProfileModel, ProfileDTO>()
                .ReverseMap()
                .ForMember(s => s.Id, opt => opt.Ignore());
        }
    }
}