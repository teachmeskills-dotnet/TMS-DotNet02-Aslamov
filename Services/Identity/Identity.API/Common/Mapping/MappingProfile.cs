using Identity.API.DTO;
using Identity.API.Models;

namespace Identity.API.Common.Mapping
{
    /// <summary>
    /// Define Automapper profile for Identity.API entities.
    /// </summary>
    public class MappingProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Constructor of Automapper profile.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<AccountModel, AccountDTO>().ReverseMap();
        }
    }
}