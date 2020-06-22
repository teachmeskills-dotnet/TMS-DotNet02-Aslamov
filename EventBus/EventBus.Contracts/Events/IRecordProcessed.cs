using EventBus.Contracts.Common;

namespace EventBus.Contracts.Events
{
    /// <summary>
    /// Define message interface for record processed event.
    /// </summary>
    public interface IRecordProcessed : IEvent
    {
        /// <summary>
        /// Message text.
        /// </summary>
        string Message { get; set; }
    }
}