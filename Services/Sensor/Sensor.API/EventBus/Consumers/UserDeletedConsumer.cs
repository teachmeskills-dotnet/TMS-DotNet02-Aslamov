using EventBus.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sensor.API.Common.Constants;
using Sensor.API.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Sensor.API.EventBus.Consumers
{
    /// <summary>
    ///  Define consumer of "user deleted" event.
    /// </summary>
    public class UserDeletedConsumer : IConsumer<IUserDeleted>
    {
        private readonly ISensorService _sensorService;
        private readonly ILogger<UserDeletedConsumer> _logger;

        /// <summary>
        /// Constructor of the consumer of "user deleted" events.
        /// </summary>
        /// <param name="sensorService">Service for sensors management.</param>
        /// <param name="logger">Logging service.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UserDeletedConsumer(ISensorService sensorService,
                                   ILogger<UserDeletedConsumer> logger)
        {
            _sensorService = sensorService ?? throw new ArgumentNullException(nameof(sensorService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Consume "user deleted" event.
        /// </summary>
        /// <param name="context">Event context.</param>
        public async Task Consume(ConsumeContext<IUserDeleted> context)
        {
            try
            {
                var profileId = context.Message.ProfileId;
                var success = await _sensorService.DeleteAllSensorsByProfileIdAsync(profileId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{SensorsConstants.EVENT_BUS_CONSUMER_ERROR}: {ex.Message}");
            }
        }
    }
}