using EventBus.Contracts.Common;
using EventBus.Contracts.DTO;
using EventBus.Contracts.Events;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Identity.API.EventBus.Produsers
{
    /// <summary>
    /// Define event produser of "user deleted" events.
    /// </summary>
    public class UserDeletedProducer : IEventProducer<IUserDeleted, IUserDTO> 
    {
        private readonly IBusControl _bus;

        /// <summary>
        /// Constructor of producer for user deletion events.
        /// </summary>
        /// <param name="bus">Event bus.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UserDeletedProducer(IBusControl bus) => _bus = bus ?? throw new ArgumentNullException(nameof(bus));

        /// <inheritdoc/>
        /// <param name="accountId">Account identifier.</param>
        public async Task<bool> Publish(IUserDTO userDTO)
        {
            try
            {
                await _bus.Publish<IUserDeleted>(new
                {
                    CommandId = Guid.NewGuid(),
                    ProfileId = userDTO.ProfileId,
                    AccountId = userDTO.AccountId,
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