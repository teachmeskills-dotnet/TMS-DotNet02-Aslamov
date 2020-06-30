using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Profile.API.Common.Constants;
using Profile.API.Common.Interfaces;
using Profile.API.DTO;
using Profile.API.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Profile.API.Services
{
    /// <summary>
    /// Define service to manage user profiles.
    /// </summary>
    public class ProfileService : IProfileService
    {
        private readonly IProfileContext _profileContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor of profile service.
        /// </summary>
        /// <param name="profileContext">Profile context.</param>
        /// <param name="mapper">Automapper.</param>
        /// <param name="logger">Logging service.</param>
        public ProfileService(IProfileContext profileContext,
                             IMapper mapper,
                             ILogger logger)
        {
            _profileContext = profileContext ?? throw new ArgumentNullException(nameof(profileContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<(Guid id, bool success)> RegisterNewProfileAsync(ProfileDTO profileDTO)
        {
            var profile = _mapper.Map<ProfileDTO, ProfileModel>(profileDTO);
            var profileFound = await _profileContext.Profiles.FirstOrDefaultAsync(p => p.AccountId == profileDTO.AccountId);

            if (profileFound != null)
            {
                _logger.Error(ProfileConstants.PROFILE_ALREADY_EXIST);
                return (Guid.Empty, false);
            }

            await _profileContext.Profiles.AddAsync(profile);
            await _profileContext.SaveChangesAsync(new CancellationToken());

            var id = profile.Id;

            return (id, true);
        }

        /// <inheritdoc/>
        public async Task<ProfileDTO> GetProfileByIdAsync(Guid id)
        {
            var profile = await _profileContext.Profiles.FirstOrDefaultAsync(p => p.Id == id);
            if(profile == null)
            {
                return null;
            }

            var profileDTO = _mapper.Map<ProfileModel, ProfileDTO>(profile);

            return profileDTO;
        }

        /// <inheritdoc/>
        public async Task<ICollection<ProfileDTO>> GetAllProfilesAsync()
        {
            var profiles = await _profileContext.Profiles.ToListAsync();
            var collectionOfProfileDTO = _mapper.Map<ICollection<ProfileModel>, ICollection<ProfileDTO>>(profiles);

            return collectionOfProfileDTO;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateProfileAsync(ProfileDTO profileDTO)
        {
            var profile = await _profileContext.Profiles.FirstOrDefaultAsync(p => p.Id == profileDTO.Id);

            if (profile == null)
            {
                return false;
            }

            profile.FirstName = profileDTO.FirstName;
            profile.LastName = profileDTO.LastName;
            profile.MiddleName = profileDTO.MiddleName;
            profile.Gender = profileDTO.Gender;
            profile.Height = profileDTO.Height;
            profile.Weight = profileDTO.Weight;

            _profileContext.Update(profile);
            await _profileContext.SaveChangesAsync(new CancellationToken());

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteProfileByIdAsync(Guid id)
        {
            var profileFound = await _profileContext.Profiles.FirstOrDefaultAsync(p => p.Id == id);
            if (profileFound == null)
            {
                _logger.Error(ProfileConstants.PROFILE_NOT_FOUND);
                return false;
            }

            _profileContext.Remove(profileFound);
            await _profileContext.SaveChangesAsync(new CancellationToken());

            return true;
        }
    }
}