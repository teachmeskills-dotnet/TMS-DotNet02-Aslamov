using EventBus.Contracts.Common;
using EventBus.Contracts.DTO;
using EventBus.Contracts.Events;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Sensor.API.EventBus.Produsers
{
    /// <summary>
    /// Define producer of "record deleted" events.
    /// </summary>
    public class RecordDeletedProducer : IEventProducer<IRecordDeleted, int> 
    {
        private readonly IBusControl _bus;

        /// <summary>
        /// Constructor of producer of "record deleted" event.
        /// </summary>
        /// <param name="bus">Event bus.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RecordDeletedProducer(IBusControl bus) => _bus = bus ?? throw new ArgumentNullException(nameof(bus));

        /// <inheritdoc/>
        /// <param name="record">Record data transfer object.</param>
        public async Task<bool> Publish(int recordId)
        {
            try
            {
                await _bus.Publish<IRecordDeleted>(new
                {
                    EventId = Guid.NewGuid(),
                    RecordID = recordId,
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