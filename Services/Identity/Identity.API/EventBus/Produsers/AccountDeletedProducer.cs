using EventBus.Contracts.Common;
using EventBus.Contracts.Events;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Identity.API.EventBus.Produsers
{
    /// <summary>
    /// Define event produser.
    /// </summary>
    public class AccountDeletedProducer : IEventProducer<IAccountDeleted, Guid> 
    {
        private readonly IBusControl _bus;

        /// <summary>
        /// Constructor of producer for account deletion events.
        /// </summary>
        /// <param name="bus">Event bus.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public AccountDeletedProducer(IBusControl bus) => _bus = bus ?? throw new ArgumentNullException(nameof(bus));

        /// <inheritdoc/>
        /// <param name="accountId">Account identifier.</param>
        public async Task<bool> Publish(Guid accountId)
        {
            try
            {
                await _bus.Publish<IAccountDeleted>(new
                {
                    CommandId = Guid.NewGuid(),
                    AccountId = accountId,
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