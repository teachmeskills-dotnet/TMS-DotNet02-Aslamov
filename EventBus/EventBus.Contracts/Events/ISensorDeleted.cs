using EventBus.Contracts.Common;

namespace EventBus.Contracts.Events
{
    /// <summary>
    /// Define message interface for sensor deletion.
    /// </summary>
    public interface ISensorDeleted : IEvent
    {
        /// <summary>
        /// Sensor Identifier.
        /// </summary>
        int SensorId { get; set; }
    }
}