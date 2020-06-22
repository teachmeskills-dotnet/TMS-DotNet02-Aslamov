using EventBus.Contracts.Common;
using EventBus.Contracts.DTO;

namespace EventBus.Contracts.Commands
{
    /// <summary>
    /// Define command to process data record.
    /// </summary>
    public interface IProcessData : ICommand
    {
        /// <summary>
        /// Data record for processing.
        /// </summary>
        IRecordDTO Record { get; set; }
    }
}