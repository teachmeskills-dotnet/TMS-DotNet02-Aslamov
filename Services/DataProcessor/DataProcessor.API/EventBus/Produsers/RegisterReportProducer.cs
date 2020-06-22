using EventBus.Contracts.Commands;
using EventBus.Contracts.Common;
using EventBus.Contracts.DTO;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace DataProcessor.API.EventBus.Produsers
{
    /// <summary>
    /// Define 
    /// </summary>
    public class RegisterReportProducer : ICommandProducer<IReportDTO> 
    {
        private readonly IBusControl _bus;

        /// <summary>
        /// Constructor of command producer.
        /// </summary>
        /// <param name="bus">Event bus.</param>
        public RegisterReportProducer(IBusControl bus) => _bus = bus ?? throw new ArgumentNullException(nameof(bus));

        /// <inheritdoc/>
        public async Task<bool> Send(IReportDTO report)
        {
            try
            {
                await _bus.Send<IRegisterReport>(new
                {
                    CommandId = Guid.NewGuid(),
                    Report = report,
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