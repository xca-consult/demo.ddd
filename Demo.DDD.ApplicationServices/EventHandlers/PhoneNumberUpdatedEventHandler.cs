using System.Threading;
using System.Threading.Tasks;
using Demo.DDD.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Demo.DDD.ApplicationServices.EventHandlers
{
    public class PhoneNumberUpdatedEventHandler : INotificationHandler<PhoneNumberUpdatedEvent>
    {
        private readonly ILogger<PhoneNumberUpdatedEventHandler> _logger;

        public PhoneNumberUpdatedEventHandler(ILogger<PhoneNumberUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(PhoneNumberUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Event {nameof(PhoneNumberUpdatedEvent)} handled");
            return Task.CompletedTask;
        }
    }
}
