using EventBus.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Report.API.Common.Constants;
using Report.API.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Report.API.EventBus.Consumers
{
    /// <summary>
    ///  Define consumer of "record deleted" events.
    /// </summary>
    public class RecordDeletedConsumer : IConsumer<IRecordDeleted>
    {
        private readonly IReportService _reportService;
        private readonly ILogger<RegisterReportConsumer> _logger;

        /// <summary>
        /// Constructor of the consumer of "record deleted" events.
        /// </summary>
        /// <param name="reportService">Service for reports management.</param>
        /// <param name="logger">Logging service.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RecordDeletedConsumer( IReportService reportService,
                                      ILogger<RegisterReportConsumer> logger)
        {
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Consume "record deleted" event.
        /// </summary>
        /// <param name="context">Event context.</param>
        public async Task Consume(ConsumeContext<IRecordDeleted> context)
        {
            try
            {
                var recordId = context.Message.RecordId;
                var success = await _reportService.DeleteAllReportsByRecordIdAsync(recordId);

                // Publish event on successful reports deleting.
                if (success)
                {
                    await context.Publish<IReportDeleted>(new
                    {
                        Message = $"{ReportConstants.DELETE_REPORT_SUCCESS} {ReportConstants.COMMAND_ID}: {context.Message.EventId}"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ReportConstants.EVENT_BUS_CONSUMER_ERROR}: {ex.Message}");
            }
        }
    }
}