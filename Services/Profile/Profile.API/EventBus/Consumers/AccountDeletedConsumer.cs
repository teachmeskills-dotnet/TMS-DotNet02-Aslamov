using EventBus.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Profile.API.Common.Constants;
using Profile.API.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Profile.API.EventBus.Consumers
{
    /// <summary>
    ///  Define consumer of "account deleted" event.
    /// </summary>
    public class AccountDeletedConsumer : IConsumer<IAccountDeleted>
    {
        private readonly IProfileService _profileService;
        private readonly ILogger<AccountDeletedConsumer> _logger;

        /// <summary>
        /// Constructor of the consumer of "account deleted" events.
        /// </summary>
        /// <param name="profileService">Service for profiles management.</param>
        /// <param name="logger">Logging service.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public AccountDeletedConsumer(IProfileService profileService,
                                      ILogger<AccountDeletedConsumer> logger)
        {
            _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Consume "account deleted" event.
        /// </summary>
        /// <param name="context">Event context.</param>
        public async Task Consume(ConsumeContext<IAccountDeleted> context)
        {
            try
            {
                var accountId = context.Message.AccountId;
                var success = await _profileService.DeleteProfileByAccountIdAsync(accountId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ProfileConstants.EVENT_BUS_CONSUMER_ERROR}: {ex.Message}");
            }
        }
    }
}