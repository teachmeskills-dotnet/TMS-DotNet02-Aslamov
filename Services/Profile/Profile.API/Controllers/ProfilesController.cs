using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profile.API.Common.Constants;
using Profile.API.Common.Interfaces;
using Profile.API.DTO;
using Serilog;

namespace Profile.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor of profiles controller.
        /// </summary>
        /// <param name="profileService">Service to manage profiles.</param>
        /// <param name="logger">Logging service.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProfilesController(IProfileService profileService, ILogger logger)
        {
            _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/profiles
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ICollection<ProfileDTO>> GetProfiles()
        {
            var profiles = await _profileService.GetAllProfilesAsync();
            var count = profiles.Count;

            _logger.Information($"{count} {ProfileConstants.GET_PROFILES}");

            return profiles;
        }

        // GET: api/profiles/{id}
        [Authorize(Roles = "User, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = await _profileService.GetProfileByIdAsync(id);
            if (profile == null)
            {
                _logger.Warning($"{id} {ProfileConstants.PROFILE_NOT_FOUND}");
                return NotFound(id);
            }

            _logger.Information($"{profile.Id} {ProfileConstants.GET_FOUND_PROFILE}");
            return Ok(profile);
        }

        // POST: api/profiles
        [Authorize(Roles ="User, Admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterNewProfile([FromBody] ProfileDTO profileDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (id, success) = await _profileService.RegisterNewProfileAsync(profileDTO);
            if (!success)
            {
                _logger.Warning($"{id} {ProfileConstants.ADD_PROFILE_CONFLICT}");
                return Conflict(id);
            }

            profileDTO.Id = id;

            _logger.Information($"{profileDTO.Id} {ProfileConstants.ADD_PROFILE_SUCCESS}");
            return CreatedAtAction(nameof(RegisterNewProfile), profileDTO);
        }

        // PUT: api/profiles/{id}
        [Authorize(Roles ="Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileDTO profileDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (profileDTO.Id == Guid.Empty)
            {
                return BadRequest(profileDTO.Id);
            }

            var profileFound = await _profileService.GetProfileByIdAsync(profileDTO.Id);
            if (profileFound == null)
            {
                _logger.Warning($"{profileDTO.Id} {ProfileConstants.PROFILE_NOT_FOUND}");
                return NotFound(profileDTO.Id);
            }

            var success = await _profileService.UpdateProfileAsync(profileDTO);
            if (!success)
            {
                _logger.Warning($"{profileDTO.Id} {ProfileConstants.UPDATE_PROFILE_CONFLICT}");
                return Conflict();
            }

            _logger.Information($"{profileDTO.Id} {ProfileConstants.UPDATE_PROFILE_SUCCESS}");
            return Ok(profileDTO);
        }

        // DELETE: api/profiles/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _profileService.DeleteProfileByIdAsync(id);
            if (!success)
            {
                _logger.Warning($"{id} {ProfileConstants.PROFILE_NOT_FOUND}");
                return NotFound(id);
            }

            _logger.Information($"{id} {ProfileConstants.DELETE_PROFILE_SUCCESS}");
            return Ok(id);
        }
    }
}