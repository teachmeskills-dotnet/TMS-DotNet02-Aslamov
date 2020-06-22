using EventBus.Contracts.Common;
using EventBus.Contracts.DTO;

namespace EventBus.Contracts.Commands
{
    /// <summary>
    /// Define interface for data processing report registration.
    /// </summary>
    public interface IRegisterReport : ICommand
    {
        /// <summary>
        /// Data processing report.
        /// </summary>
        IReportDTO Report { get; set; }
    }
}