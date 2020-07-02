using EventBus.Contracts.Events;
using Identity.API.Common.Constants;
using Identity.API.Common.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Identity.API.EventBus.Consumers
{
    /// <summary>
    ///  Define consumer of "user deleted" event.
    /// </summary>
    public class UserDeletedConsumer : IConsumer<IUserDeleted>
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<UserDeletedConsumer> _logger;

        /// <summary>
        /// Constructor of the consumer of "user deleted" events.
        /// </summary>
        /// <param name="accountService">Service for account management.</param>
        /// <param name="logger">Logging service.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UserDeletedConsumer(IAccountService accountService,
                                   ILogger<UserDeletedConsumer> logger)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
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
                var accountId = context.Message.AccountId;
                var success = await _accountService.DeleteAccountByIdAsync(accountId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{AccountConstants.EVENT_BUS_CONSUMER_ERROR}: {ex.Message}");
            }
        }
    }
}