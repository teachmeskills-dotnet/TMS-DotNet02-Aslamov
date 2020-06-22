using DataProcessor.API.Common.Interfaces;
using EventBus.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DataProcessor.API.EventBus.Consumers
{
    /// <summary>
    ///  Define consumer of record registration events.
    /// </summary>
    public class NewRecordRegisteredConsumer : IConsumer<IRecordRegistered>
    {
        private readonly IDataProcessorService _dataProcessorService;
        private readonly ILogger<NewRecordRegisteredConsumer> _logger;

        /// <summary>
        /// Constructor of the consumer of record registration events.
        /// </summary>
        /// <param name="dataProcessorService">Service for data processing.</param>
        /// <param name="logger">Logging service.</param>
        public NewRecordRegisteredConsumer( IDataProcessorService dataProcessorService,
                                            ILogger<NewRecordRegisteredConsumer> logger)
        {
            _dataProcessorService = dataProcessorService ?? throw new ArgumentNullException(nameof(dataProcessorService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Consume record registration event.
        /// </summary>
        /// <param name="context">Event context.</param>
        public async Task Consume(ConsumeContext<IRecordRegistered> context)
        {
            try
            {
                var newRecord = context.Message.Record;
                //var (reportDTO, success) = await _dataProcessorService.ProcessData(newRecord);

                await context.RespondAsync<IRecordProcessed>(new
                {
                    Text = $"Received: {context.Message.MessageId}"
                });

                // TODO: publish recordDTO to Record.API below.
                // ...

            }
            catch (Exception ex)
            {
                _logger.LogError("ProductChangedConsumerError", ex);
            }
        }
    }
}