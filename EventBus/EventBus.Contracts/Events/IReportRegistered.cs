using EventBus.Contracts.Common;
using EventBus.Contracts.DTO;

namespace EventBus.Contracts.Events
{
    /// <summary>
    /// Define message interface for new report registration.
    /// </summary>
    public interface IReportRegistered : IEvent
    {
        /// <summary>
        /// New report data transfer object.
        /// </summary>
        IReportDTO Report { get; set; }
    }
}