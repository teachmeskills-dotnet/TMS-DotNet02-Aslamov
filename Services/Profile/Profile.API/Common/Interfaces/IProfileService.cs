using Profile.API.DTO;
using System;
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
        Task<(Guid id, bool success)> RegisterNewProfileAsync(ProfileDTO profileDTO);

        /// <summary>
        /// Get profile by identifier.
        /// </summary>
        /// <param name="id">Profile identifier.</param>
        /// <returns>Profile object.</returns>
        Task<ProfileDTO> GetProfileByIdAsync(Guid id);


        /// <summary>
        /// Get profile by account identifier.
        /// </summary>
        /// <param name="accountId">Account identifier.</param>
        /// <returns>Profile object.</returns>
        Task<ProfileDTO> GetProfileByAccountIdAsync(Guid accountId);

        /// <summary>
        /// Get all registered profiles.
        /// </summary>
        /// <returns>Profiles collection.</returns>
        Task<ICollection<ProfileDTO>> GetAllProfilesAsync();

        /// <summary>
        /// Update profile information.
        /// </summary>
        /// <param name="profileDTO">Profile object.</param>
        /// <returns>Operation status.</returns>
        Task<bool> UpdateProfileAsync(ProfileDTO profileDTO);

        /// <summary>
        /// Delete profile from application.
        /// </summary>
        /// <param name="id">Profile identifier.</param>
        /// <returns>Operation status.</returns>
        Task<bool> DeleteProfileByIdAsync(Guid id);

        /// <summary>
        /// Delete profile from application by account id.
        /// </summary>
        /// <param name="accountId">Account identifier.</param>
        /// <returns>Operation status.</returns>
        Task<bool> DeleteProfileByAccountIdAsync(Guid accountId);
    }
}