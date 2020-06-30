using System;

namespace EventBus.Contracts.DTO
{
    /// <summary>
    /// Interface for user Data Transfer Object.
    /// </summary>
    public interface IUserDTO
    {
        /// <summary>
        /// User profile Id.
        /// </summary>
        Guid ProfileId { get; set; }

        /// <summary>
        /// User account Id.
        /// </summary>
        Guid AccountId { get; set; }
    }
}