using Profile.API.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Profile.API.Common.Interfaces
{
    /// <summary>
    /// Interface to manage profiles.
    /// </summary>
    public interface IProfileService
    {
        /// <summary>
        /// Register new profile.
        /// </summary>
        /// <param name="profileDTO">Profile model.</param>
        /// <returns>Profile Id and operation status.</returns>
        Task<(int id, bool success)> RegisterNewProfileAsync(ProfileDTO profileDTO);

        /// <summary>
        /// Get profile by identifier.
        /// </summary>
        /// <param name="id">Profile identifier.</param>
        /// <returns>Profile object.</returns>
        Task<ProfileDTO> GetProfileByIdAsync(int id);

        /// <summary>
        /// Get all registered profiles.
        /// </summary>
        /// <returns>Profiles collection.</returns>
        Task<ICollection<ProfileDTO>> GetAllProfilesAsync();

        /// <summary>
        /// Update profile information.
        /// </summary>
        /// <param name="sensor">Profile object.</param>
        /// <returns>Operation status.</returns>
        Task<bool> UpdateProfileAsync(ProfileDTO sensor);

        /// <summary>
        /// Delete profile from application.
        /// </summary>
        /// <param name="id">Profile identifier.</param>
        /// <returns>Operation status.</returns>
        Task<bool> DeleteProfileByIdAsync(int id);
    }
}