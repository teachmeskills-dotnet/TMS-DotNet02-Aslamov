using EventBus.Contracts.Common;
using EventBus.Contracts.DTO;
using EventBus.Contracts.Events;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Sensor.API.EventBus.Produsers
{
    /// <summary>
    /// Define producer of "sensor deleted" events.
    /// </summary>
    public class SensorDeletedProducer : IEventProducer<ISensorDeleted, int> 
    {
        private readonly IBusControl _bus;

        /// <summary>
        /// Constructor of "sensor deleted" event producer.
        /// </summary>
        /// <param name="bus">Event bus.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SensorDeletedProducer(IBusControl bus) => _bus = bus ?? throw new ArgumentNullException(nameof(bus));

        /// <inheritdoc/>
        /// <param name="sensorID">Sensor identifier.</param>
        public async Task<bool> Publish(int sensorId)
        {
            try
            {
                await _bus.Publish<ISensorDeleted>(new
                {
                    EventId = Guid.NewGuid(),
                    SensorId = sensorId,
                    CreationDate = DateTime.Now,
                });
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}