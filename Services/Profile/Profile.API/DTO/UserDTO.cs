using EventBus.Contracts.DTO;
using System;

namespace Profile.API.DTO
{
    /// <summary>
    /// DTO of user parameters.
    /// </summary>
    public class UserDTO : IUserDTO
    {
        /// <inheritdoc/>
        public Guid ProfileId { get; set; }

        /// <inheritdoc/>
        public Guid AccountId { get; set; }
    }
}