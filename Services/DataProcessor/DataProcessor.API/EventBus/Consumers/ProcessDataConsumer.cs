using DataProcessor.API.Common.Constants;
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
        /// <exception cref="ArgumentNullException"></exception>
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
                var recordTO = context.Message.Record;
                var (reportDTO, success) = await _dataProcessorService.ProcessData(recordTO);

                // Publish event on successful data processing.
                await context.Publish<IRecordProcessed>(new
                {
                    Message = $"{DataProcessorConstants.DATA_PROCESSING_SUCCESS} {DataProcessorConstants.COMMAND_ID}: {context.Message.CommandId}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DataProcessorConstants.EVENT_BUS_CONSUMER_ERROR}: {ex.Message}");
            }
        }
    }
}