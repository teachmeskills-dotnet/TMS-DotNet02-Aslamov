using EventBus.Contracts.Common;
using System;

namespace EventBus.Contracts.Events
{
    /// <summary>
    /// Define message interface for user deletion.
    /// </summary>
    public interface IUserDeleted : IEvent
    {
        /// <summary>
        /// User profile Identifier (for Profile.API).
        /// </summary>
        Guid ProfileId { get; set; }

        /// <summary>
        /// Usre account Identifier for (Identity.API).
        /// </summary>
        Guid AccountId { get; set; }
    }
}