using EventBus.Contracts.DTO;
using System;

namespace EventBus.Contracts.Events
{
    /// <summary>
    /// Define message interface for new record registration.
    /// </summary>
    public interface IRecordRegistered
    {
        /// <summary>
        /// Message identifier.
        /// </summary>
        Guid MessageId { get; set; }

        /// <summary>
        /// New record data transfer object.
        /// </summary>
        IRecordDTO Record { get; set; }

        /// <summary>
        /// Message creation date.
        /// </summary>
        DateTime CreationDate { get; set; }
    }
}