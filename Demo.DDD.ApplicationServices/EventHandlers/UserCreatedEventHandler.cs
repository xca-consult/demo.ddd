using System.Threading;
using System.Threading.Tasks;
using Demo.DDD.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Demo.DDD.ApplicationServices.EventHandlers
{
    public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ILogger<PhoneNumberUpdatedEventHandler> _logger;

        public UserCreatedEventHandler(ILogger<PhoneNumberUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Event {nameof(UserCreatedEvent)} handled");
            return Task.CompletedTask;
        }
      
    }
}
