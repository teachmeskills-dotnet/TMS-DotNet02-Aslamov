using EventBus.Contracts.Common;

namespace EventBus.Contracts.Events
{
    /// <summary>
    /// Define message interface for report deletion.
    /// </summary>
    public interface IReportDeleted : IEvent
    {
        /// <summary>
        /// Report Identifier.
        /// </summary>
        int ReportId { get; set; }
    }
}