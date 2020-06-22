using DataProcessor.API.Common.Interfaces;
using EventBus.Contracts.Commands;
using EventBus.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DataProcessor.API.EventBus.Consumers
{
    /// <summary>
    ///  Define consumer of process data command.
    /// </summary>
    public class ProcessDataConsumer : IConsumer<IProcessData>
    {
        private readonly IDataProcessorService _dataProcessorService;
        private readonly ILogger<ProcessDataConsumer> _logger;

        /// <summary>
        /// Constructor of the consumer of record registration events.
        /// </summary>
        /// <param name="dataProcessorService">Service for data processing.</param>
        /// <param name="logger">Logging service.</param>
        public ProcessDataConsumer( IDataProcessorService dataProcessorService,
                                    ILogger<ProcessDataConsumer> logger)
        {
            _dataProcessorService = dataProcessorService ?? throw new ArgumentNullException(nameof(dataProcessorService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Consume record registration event.
        /// </summary>
        /// <param name="context">Event context.</param>
        public async Task Consume(ConsumeContext<IProcessData> context)
        {
            try
            {
                var dataDTO = context.Message.Record;
                var (reportDTO, success) = await _dataProcessorService.ProcessData(dataDTO);

                // Publish event on successful data processing.
                await context.Publish<IRecordProcessed>(new
                {
                    Message = $"Data has been processed. Event ID: {context.Message.CommandId}"
                });

                // Send command to register new data processing report.
                await context.Send<IRegisterReport>(new
                {
                    CommandId = Guid.NewGuid(),
                    Report = reportDTO,
                    CreationDate = DateTime.Now,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Event Bus ERROR", ex);
            }
        }
    }
}