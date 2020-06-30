using EventBus.Contracts.Common;
using System;

namespace EventBus.Contracts.Events
{
    /// <summary>
    /// Define message interface for account deletion.
    /// </summary>
    public interface IAccountDeleted : IEvent
    {
        /// <summary>
        /// Account Identifier.
        /// </summary>
        Guid AccountId { get; set; }
    }
}