using EventBus.Contracts.Commands;
using EventBus.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Report.API.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Report.API.EventBus.Consumers
{
    /// <summary>
    ///  Define consumer of register report command.
    /// </summary>
    public class RegisterReportConsumer : IConsumer<IRegisterReport>
    {
        private readonly IReportService _reportService;
        private readonly ILogger<RegisterReportConsumer> _logger;

        /// <summary>
        /// Constructor of the consumer of report registration command.
        /// </summary>
        /// <param name="reportService">Service for reports management.</param>
        /// <param name="logger">Logging service.</param>
        public RegisterReportConsumer( IReportService reportService,
                                       ILogger<RegisterReportConsumer> logger)
        {
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Consume report registration event.
        /// </summary>
        /// <param name="context">Event context.</param>
        public async Task Consume(ConsumeContext<IRegisterReport> context)
        {
            try
            {
                var reportDTO = context.Message.Report;
                var (id, success) = await _reportService.RegisterNewReportAsync(reportDTO);

                // Publish event on successful data processing.
                await context.Publish<IReportRegistered>(new
                {
                    Message = $"Report has been registered. Event ID: {context.Message.CommandId}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Event Bus ERROR", ex);
            }
        }
    }
}