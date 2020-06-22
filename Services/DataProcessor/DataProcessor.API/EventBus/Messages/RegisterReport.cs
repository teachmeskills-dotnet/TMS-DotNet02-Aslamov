using EventBus.Contracts.Commands;
using EventBus.Contracts.DTO;
using System;

namespace DataProcessor.API.EventBus.Messages
{
    /// <summary>
    /// Define command to register data processing reports.
    /// </summary>
    public class RegisterReport : IRegisterReport
    {
        /// <inheritdoc/>
        public IReportDTO Report { get; set; }

        /// <inheritdoc/>
        public Guid CommandId { get; set; }

        /// <inheritdoc/>
        public DateTime CreationDate { get; set; }
    }
}