using EventBus.Contracts.Common;

namespace EventBus.Contracts.Events
{
    /// <summary>
    /// Define message interface for record deletion.
    /// </summary>
    public interface IRecordDeleted : IEvent
    {
        /// <summary>
        /// Record Identifier.
        /// </summary>
        int RecordId { get; set; }
    }
}