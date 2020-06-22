using System;

namespace EventBus.Contracts.Common
{
    /// <summary>
    /// Define interface for message broker events.
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// Event Identifier.
        /// </summary>
        Guid EventId { get; set; }

        /// <summary>
        /// Event creation date.
        /// </summary>
        DateTime CreationDate { get; set; }
    }
}